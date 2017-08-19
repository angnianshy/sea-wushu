using Adre.Controls.ResultList.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adre.Controls.ResultList
{
    public interface IDataContext
    {
        event SelectedItemChangedHandler SelectedItemChanged;

        ICollection<IItemViewModel> Items { get; set; }

        IItemViewModel CreateAndAdd(StartList.IItemViewModel startListItem);

        void Clear();
        
        IItemViewModel SelectedItem { get; set; }
    }
}
