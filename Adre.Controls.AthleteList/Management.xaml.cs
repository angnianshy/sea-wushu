using System;
using System.Windows;
using System.Windows.Controls;

namespace Adre.Controls.AthleteList
{
    /// <summary>
    /// Interaction logic for Management.xaml
    /// </summary>
    public partial class ManagementControl : UserControl
    {
        DataContext _dataContext;

        public event DataRefreshedHandler OnDataContextRefreshed;
        public event DataContextBindedHandler OnDataContextBinded;
        public event DataUpdatedHandler OnDataUpdated;
        
        public ManagementControl()
        {
            _dataContext = new DataContext();
            DataContext = _dataContext;
            InitializeComponent();
            Loaded += OnLoaded;
        }
        
        public void OnLoaded(object sender, RoutedEventArgs args)
        {
            OnDataContextBinded?.Invoke(_dataContext);
        }

        public void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            OnDataContextRefreshed?.Invoke(_dataContext);
        }

        private void RadGridView_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
        {
            OnDataUpdated?.Invoke(e.NewData);
        }
    }
}
