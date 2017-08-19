using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Adre.Controls.RankingList;
using Adre.SEA.Database;
using Adre.SEA.Reports;
using Prism.Mvvm;
using Telerik.Reporting;

namespace Adre.Controls.RankingList
{
    /// <summary>
    /// Interaction logic for RankingListControl.xaml
    /// </summary>
    public partial class RankingListControl : UserControl
    {
        public event DataContextBindedHandler OnDataContextBinded;
        public event DataSavedHandler OnDataSaved;

        private RankingListControlViewModel Dc => (RankingListControlViewModel) DataContext;

        public RankingListControl()
        {
            InitializeComponent();
            DataContext = new RankingListControlViewModel();
        }

        private void RankingListControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            Dc.Load();
            Dc.DataContext = new RankingList.DataContext();
            OnDataContextBinded?.Invoke(Dc.DataContext);
        }

        private void ButtonRoundRobinReportClick(object sender, RoutedEventArgs e)
        {
            //InstanceReportSource reportSource = ReportManager.GenerateRoundRobin(Dc.SelectedEvent.Id, Dc.SelectedMatchGroupType.Id);
            //var reportWindow = new ReportWindow(reportSource);
            //reportWindow.ShowDialog();
        }

        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            OnDataSaved?.Invoke(Dc.DataContext);
        }

        private void OnMedalTallyReportClick(object sender, RoutedEventArgs e)
        {
            InstanceReportSource reportSource = ReportManager.GenerateMedalTallyReport();
            var reportWindow = new ReportWindow(reportSource);
            reportWindow.ShowDialog();
        }
    }

    public class RankingListControlViewModel : BindableBase
    {
        private ObservableCollection<Event> _events = new ObservableCollection<Event>();

        private Event _selectedEvent;

        IDataContext _dataContext;

        public ObservableCollection<Event> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        public Event SelectedEvent
        {
            get => _selectedEvent;
            set => SetProperty(ref _selectedEvent, value);
        }

        public IDataContext DataContext
        {
            get => _dataContext;
            set => SetProperty(ref _dataContext, value);
        }

        public void Load()
        {
            using (var ctx = new ASEAContext())
            {
                Events = new ObservableCollection<Event>(ctx.Events.OrderBy(m => m.Name));
                SelectedEvent = Events.FirstOrDefault();
            }
        }
    }
}
