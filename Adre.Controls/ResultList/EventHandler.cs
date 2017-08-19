
namespace Adre.Controls.ResultList.Team
{
    public delegate void DataContextBindingHandler(IDataContext context);
    public delegate void DataSavedHandler(IDataContext context);
    public delegate void SelectedItemChangedHandler(IItemViewModel item);
    public delegate void DataRefreshedHandler(IDataContext context);
    public delegate void RowDoubleClickedHandler(IItemViewModel item);
}
