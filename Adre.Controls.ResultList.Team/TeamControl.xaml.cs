using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Adre.SEA.Database;
using Adre.SEA.Reports;
using Telerik.Reporting;
using Telerik.Windows.Controls;

namespace Adre.Controls.ResultList.Team
{
  /// <summary>
  /// Interaction logic for TeamControl.xaml
  /// </summary>
  public partial class TeamControl : UserControl, IResultListButtonHandler
  {
    DataContext _dataContext;
    public event DataContextBindingHandler OnDataContextBinded;
    public event DataSavedHandler OnDataSaved;
    public event DataRefreshedHandler OnDataRefreshed;
    public event RowDoubleClickedHandler OnDataRowDoubleClicked;

    public TeamControl()
    {
      _dataContext = new DataContext();
      DataContext = _dataContext;
      InitializeComponent();
      Loaded += OnLoaded;
    }

    void OnLoaded(object sender, RoutedEventArgs e)
    {
      OnDataContextBinded?.Invoke(_dataContext);
    }

    public void BtnReport_Click(object sender, RoutedEventArgs e)
    {
      throw new NotImplementedException();
    }

    public void LstAllMatchesReport_OnPreviewMouseDown(object sender, RoutedEventArgs e)
    {
      throw new NotImplementedException();
    }

    public void LstMatchReport_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
      throw new NotImplementedException();
    }

    public void OnSaveClicked(object sender, EventArgs e)
    {
      OnDataSaved?.Invoke(_dataContext);
    }

    public void OnRefresh(object sender, EventArgs e)
    {
      OnDataRefreshed?.Invoke(_dataContext);
    }

    private void ButtonDetailReport(object sender, RoutedEventArgs e)
    {
      if (_dataContext.SelectedItem == null)
        return;


      var selectedMatchId = _dataContext.SelectedItem.StartListItem.Id;

      using (var ctx = new ASEAContext())
      {
        var match = ctx.Matches.First(m => m.Id == selectedMatchId);
        var matchNo = match.MatchNo;
        var hasMoreThanOneMatchWithSameEvent = ctx.Matches.Count(m => m.MatchNo == matchNo && m.Event.Id == match.Event.Id) > 1;

        InstanceReportSource reportSource;

        if (hasMoreThanOneMatchWithSameEvent) reportSource = ReportManager.GenerateResultDetailListTeamEvent(selectedMatchId);
        else reportSource = ReportManager.GenerateResultDetailList(selectedMatchId);

        var reportWindow = new ReportWindow(reportSource);

        reportWindow.ShowDialog();
      }
    }

    public void OnSearchBoxChanged(object sender, EventArgs e)
    {
      var tb = (System.Windows.Controls.TextBox)sender;

      if (tb.Text.Length > 0)
      {
        var items = _dataContext.Items.Where(m => m.StartListItem.No.ToString().Contains(tb.Text));
      }

    }

    private void ButtonEventReport(object sender, RoutedEventArgs e)
    {
      var selectedMatchId = _dataContext.SelectedItem.StartListItem.Id;
      var reportWindow = new ReportWindow(ReportManager.GenerateResultListOverall(selectedMatchId));
      reportWindow.ShowDialog();
    }
  }
}
