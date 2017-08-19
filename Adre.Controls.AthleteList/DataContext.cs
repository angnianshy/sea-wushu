using System.Collections.Generic;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Adre.Controls.AthleteList
{
    class DataContext: BindableBase, IDataContext
    {
        ICollection<IAthlete> _athleteList;

        public DataContext()
        {
            AthleteList = new ObservableCollection<IAthlete>();
        }

        public ICollection<IAthlete> AthleteList
        {
            get => _athleteList;
            set => SetProperty(ref _athleteList, value);
        }
        
    }
}
