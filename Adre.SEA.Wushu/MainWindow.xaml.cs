using System.Windows;
using Adre.SEA.Database;
using System.Collections.ObjectModel;
using Adre.Controls;
using System.Data.Entity.Migrations;
using System;
using System.Linq;
using System.Net;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Threading;
using Adre.SEA.Provider;

namespace Adre.SEA.Wushu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ASEAContext _dbContext;
        IService _athleteService;

        Controls.StartList.IDataContext _startListContext;
        Controls.ResultList.IDataContext _resultListContext;
        Controls.RankingList.IDataContext _rankingListContext;

        Controls.StartList.IItemViewModel _selectedStartListItem;
        Controls.ResultList.IItemViewModel _selectedResultListItem;
        DataContext _dataContext;

        public MainWindow()
        {
            _dbContext = new ASEAContext();
            _dataContext = new DataContext();
            DataContext = _dataContext;
            _athleteService = new AthleteService();

            InitializeComponent();

            new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                _dataContext.Time = DateTime.Now.ToString("HH:mm:ss");
            }, this.Dispatcher);
        }

        public void OnNewItemAdded(Controls.StartList.IItemViewModel item)
        {
            item.ReservedEventList = new ObservableCollection<IEvent>(_dbContext.Events);
            item.GenderList = new ObservableCollection<string>(item.ReservedEventList.Select(m => m.Gender).Distinct());
            item.SelectedGender = item.GenderList.FirstOrDefault();
            item.IPhaseList = new ObservableCollection<IPhase>(_dbContext.Phases.OrderBy(m => m.Order));
            
            _resultListContext.CreateAndAdd(item);
            _startListContext.SelectedItem = item;
        }

        public void OnStartListDataBinded(Controls.StartList.IDataContext context)
        {
            if (_startListContext == null)
                _startListContext = context;
        }

        public void OnResultListDataBinded(Controls.ResultList.IDataContext context)
        {
            if (_resultListContext == null)
            {
                _resultListContext = context;
                LoadAllDataFromDB();
            }
        }

    void LoadStartListDataFromDB()
    {
      _startListContext.Clear();
      _resultListContext.Clear();

      var matches = _dbContext.Matches.OrderBy(m => m.MatchNo);

      foreach (var m in matches)
      {
        var newMatch = _startListContext.NewItem();
        var mr = _dbContext.Result.Where(x => x.Match.Id == m.Id).FirstOrDefault();
        var newMr = _resultListContext.CreateAndAdd(newMatch);

        newMatch.Id = m.Id;
        newMatch.ReservedEventList = new ObservableCollection<IEvent>(_dbContext.Events);
        newMatch.Arena = m.Arena;
        newMatch.DateTimeStart = m.DateTimeStart;
        newMatch.No = m.MatchNo;
        newMatch.Remarks = m.Remarks;
        newMatch.GenderList = new ObservableCollection<string>(_dbContext.Events.Select(x => x.Gender).OrderBy(x => x).Distinct());
        newMatch.SelectedGender = m.Event.Gender;
        newMatch.IEventList = new ObservableCollection<IEvent>(_dbContext.Events.OrderBy(x => x.Name));
        newMatch.SelectedEvent = m.Event;
        newMatch.IPhaseList = new ObservableCollection<IPhase>(_dbContext.Phases.OrderBy(x => x.Order));
        newMatch.SelectedPhase = m.Phase;
        newMatch.Venue = m.Venue;

        newMatch.AthleteList = new ObservableCollection<IAthlete>(m.Event.Athletes);
        newMatch.SelectedAthlete = m.Athletes.FirstOrDefault();

        _startListContext.Items.Add(newMatch);

        if (mr != null)
        {
          newMr.Id = mr.Id;
          newMr.DD = mr.DD;
          newMr.FinalScore = mr.FinalScore;
          newMr.OB = mr.OB;
          newMr.Penalty = mr.Penalty;
          newMr.QM = mr.QM;
          newMr.Remarks = mr.Remarks;
          newMr.Medal = mr.Medal;     //angns
        }
      }
    }


    void SaveStartListToDB(object sender, EventArgs e)
        {
            if (_startListContext.SelectedItem == null) return;
            var item = _startListContext.SelectedItem;

            var match = _dbContext.Matches.Find(item.Id);
            if (match == null)
            {
                match = new Match();
                match.Id = item.Id;
                var result = new Result();
                result.Id = Guid.NewGuid();
                result.Match = match;
                _dbContext.Result.AddOrUpdate(result);
            }

            match.Arena = item.Arena;
            match.DateTimeStart = item.DateTimeStart;
            match.Event = (Event)item.SelectedEvent;
            match.MatchNo = item.No;
            match.Remarks = item.Remarks;
            match.Phase = (Phase)item.SelectedPhase;
            match.Venue = item.Venue;
            _dbContext.Matches.AddOrUpdate(match);

            //save athletes
            match.Athletes.RemoveAll(m => m.Matches.Contains(match));
            match.Athletes.Add((Athlete)item.SelectedAthlete);

            _dbContext.SaveChanges();
        }

    void SaveResultListToDB(object sender, EventArgs e)
    {
      if (_resultListContext.SelectedItem == null) return;

      var item = _resultListContext.SelectedItem;

      var result = new Result();
      if (item.Id != Guid.Empty) result = _dbContext.Result.Find(item.Id);


      result.Match = _dbContext.Matches.Find(item.StartListItem.Id);
      if (result.Match == null) return;

      result.DD = item.DD;
      result.OB = item.OB;
      result.QM = item.QM;
      result.Penalty = item.Penalty;
      result.FinalScore = item.FinalScore;
      result.Remarks = item.Remarks;
      result.Medal = item.Medal; //angns

      _dbContext.Result.AddOrUpdate(result);
      _dbContext.SaveChanges();
    }


    void OnStartListSaved(Controls.StartList.IDataContext context) { 
            SaveStartListToDB(null, null);
            MessageBox.Show("Save Completed!", "Status");
        }

        void OnResultListSaved(Controls.ResultList.IDataContext context)
        {
            SaveResultListToDB(null, null);
            SaveRankingListToDB();
            MessageBox.Show("Save Completed!", "Status");
        }

        void OnRankingListSave(Controls.RankingList.IDataContext context) {
            SaveRankingListToDB();
            MessageBox.Show("Save Completed!", "Status");
        }

        void LoadAllDataFromDB()
        {
            _dbContext.Dispose();
            _dbContext = new ASEAContext();

            _selectedStartListItem = _startListContext.SelectedItem;
            _selectedResultListItem = _resultListContext.SelectedItem;

            LoadStartListDataFromDB();
            LoadRankingList();

            _startListContext.SelectedItem = _startListContext.Items.Where(m => m.Id == _selectedStartListItem?.Id).FirstOrDefault();
            _resultListContext.SelectedItem = _resultListContext.Items.Where(m => m.Id == _selectedResultListItem?.Id).FirstOrDefault();

            _selectedStartListItem = _startListContext.SelectedItem;
            _selectedResultListItem = _resultListContext.SelectedItem;
        }
        
        void OnResultListRowDoubleClicked(Controls.ResultList.IItemViewModel item)
        {
            if (item != null) SendtMatchDetailsToScoring(item.StartListItem);
        }

        void SendtMatchDetailsToScoring(Controls.StartList.IItemViewModel item)
        {
            MessageBoxResult result = MessageBox.Show("Send info of Match No " + item.No + " to RealTime?", "Confirmation", MessageBoxButton.YesNo);

            if (result != MessageBoxResult.Yes) return;

            var t = Task.Run(() =>
            {
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        byte[] response =

                        client.UploadValues(ConfigurationManager.AppSettings["RealtimeServerTerminal"] + "/" + item.Id, new NameValueCollection()
                        {
                        { "EventName", item.SelectedEvent.ToString() },
                        { "MatchNo", item.No.ToString() },
                        { "ContingentCode", item.SelectedAthlete.IContingent.Code },
                        { "ContingentName", item.SelectedAthlete.IContingent.Name },
                        { "Series", item.SelectedPhase.ToString() },
                        { "StartTime", item.DateTimeStart.ToString("HH:mm") },
                        { "Date", item.DateTimeStart.ToString("dd/MM/yyyy") },
                        { "Athlete", item.SelectedAthlete.ToString() },
                        { "SportName", "WUSHU"}
                        });

                        Console.WriteLine(System.Text.Encoding.UTF8.GetString(response));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            });
        }

         bool? OnStartListItemDeleted(Controls.StartList.IItemViewModel item)
         {
              
            if (item == null) return false;

            MessageBoxResult result = MessageBox.Show("Are you sure to delete Match No " + item.No + "?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                using (var d = new ASEAContext())
                {
                    var match = d.Matches.Find(item.Id);

                    if (match != null)
                    {
                        d.Result.RemoveRange(d.Result.Where(m => m.Match.Id == match.Id));

                        if (match.Event != null)
                            d.Rankings.RemoveRange(d.Rankings.Where(m => m.Event.Id == match.Event.Id));

                        d.Matches.Remove(match);
                        d.SaveChanges();
                    }
                }

                var results = _resultListContext.Items.Where(m => m.StartListItem.Id == item.Id).ToList();

                foreach (var r in results) _resultListContext.Items.Remove(r);

                _resultListContext.Items = _resultListContext.Items;

                return true;
            }

            return false;
        }

        void OnStartListDataRefreshed(Controls.StartList.IDataContext dataContext) {
            LoadAllDataFromDB();
            MessageBox.Show("Data Refreshed!", "Status");

        }

        void OnResultListDataRefreshed(Controls.ResultList.IDataContext dataContext) {
            LoadAllDataFromDB();
            MessageBox.Show("Data Refreshed!", "Status");
        }

        void OnRankingDataContextBinded(Controls.RankingList.IDataContext context)
        {
            _rankingListContext = context;
            LoadAllDataFromDB();
        }

        void LoadRankingList()
        {
            if (_rankingListContext == null) return;
            _rankingListContext.Clear();

            var matches = new List<Match>(_dbContext.Matches);
            var container = new List<Controls.RankingList.IItemViewModel>();
            var rankingItems = _rankingListContext.Items;

            foreach (var c in matches)
            {
                if (c.Result == null || c.Athletes == null || c.Athletes.Count == 0) continue;
                var contingent = c.Athletes.FirstOrDefault()?.Contingent;

                var dbItem = _dbContext.Rankings.Where(m => m.Contingent.Id == contingent.Id &&
                    c.Event.Id == m.Event.Id
                ).FirstOrDefault();

                var item = _rankingListContext.Create();
                item.Contingent = c.Athletes.First().Contingent;
                item.Athlete = c.Athletes.FirstOrDefault();
                item.Event = c.Event;
                item.SelectedMedal = dbItem == null ? "" : dbItem.Medal;
                item.FinalScore = dbItem != null && dbItem.FinalScore > 0 ? dbItem.FinalScore : c.Result.FinalScore;
                item.No = dbItem == null ? 0 : dbItem.Order;
                _rankingListContext.Add(item);
            }

            _rankingListContext.Sort();
            _rankingListContext.Ranking();
        }

        void SaveRankingListToDB()
        {
            foreach (var i in _rankingListContext.Items)
            {
                var ranking = _dbContext.Rankings.Where(x => x.Contingent.Id == i.Contingent.Id &&
                    i.Event.Id == x.Event.Id).FirstOrDefault();

                if (ranking == null)
                {
                    ranking = new Ranking();
                    ranking.Id = Guid.NewGuid();
                }

                ranking.Event = (Event)i.Event;
                ranking.Contingent = (Contingent)i.Contingent;
                ranking.Athlete = (Athlete)i.Athlete;
                ranking.Order = i.No;
                ranking.FinalScore = i.FinalScore;
                ranking.Medal = i.SelectedMedal;
                _dbContext.Rankings.AddOrUpdate(ranking);
            }

            _dbContext.SaveChanges();
            LoadAllDataFromDB();
        }

        void OnImporterStatusChanged(string status, float value)
        {
            _dataContext.Visibility = "Collapsed";
            _dataContext.Status = status;

            new DispatcherTimer(new TimeSpan(0, 0, 3), DispatcherPriority.Normal, (object sender, EventArgs e) =>
            {
                _dataContext.Status = "";
                ((DispatcherTimer)sender).Stop();
            }, this.Dispatcher);
        }

        void OnImporterStatusChanging(string status, float value)
        {
            _dataContext.Visibility = "Visible";
            _dataContext.Status = status;
            _dataContext.Value = value * 100;
        }

        void OnAthleteManagementDataContextBinded(Controls.AthleteList.IDataContext context)
        {
            _athleteService.Register(context);
            _athleteService.Load();
        }

        void OnAthleteManagementDataContextRefreshed(Controls.AthleteList.IDataContext context)
        {
            _athleteService.Refresh();
        }

        void OnAthleteUpdated(object athlete)
        {
            dynamic a = athlete;
            ((AthleteService)_athleteService).Update(a.Id, a);
        }

        
  }
}
