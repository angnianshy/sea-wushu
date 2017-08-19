using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Adre.Controls.StartList.Team
{
    public class ItemViewModel : BaseItemModel, IItemViewModel, ICloneable
    {
        public event AthleteChangedHandler OnAthleteChanged;
        
        ObservableCollection<IAthlete> _athleteList;

        IAthlete _selectedAthlete;

        ICollection<string> _genderList;

        string _selectedGender;
                
        public ItemViewModel()
        {
            AthleteList = new ObservableCollection<IAthlete>();
        }

        public override IEvent SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                if (value == null) return;
                AthleteList = new ObservableCollection<IAthlete>(value.IAthletes.OrderBy(m => m.IContingent.Code).ThenBy(m => m.PreferredName));
                SetProperty(ref _selectedEvent, value);
            }
        }
        
        public ICollection<IEvent> ReservedEventList { get; set; }

        public IAthlete SelectedAthlete { get => _selectedAthlete; set => SetProperty(ref _selectedAthlete, value); }

        public ObservableCollection<IAthlete> AthleteList { get => _athleteList; set => SetProperty(ref _athleteList, value); }
        
        public ObservableCollection<IEvent> IEventList { get => EventList; set { EventList = value; } }

        public ObservableCollection<IPhase> IPhaseList { get => PhaseList; set { PhaseList = value; } }

        public ICollection<string> GenderList { get => _genderList; set { SetProperty(ref _genderList, value); } }

        public string SelectedGender
        {
            get => _selectedGender;
            set
            {
                SetProperty(ref _selectedGender, value);
                if (value != null)
                    EventList = new ObservableCollection<IEvent>(ReservedEventList.Where(m => m.Gender == value).OrderBy(m => m.Name));
                else
                    EventList = new ObservableCollection<IEvent>();
            }
        }

        public object Clone()
        {
            var cloned = (ItemViewModel)this.MemberwiseClone();
            cloned.Id = Guid.NewGuid();
            cloned.AthleteList = new ObservableCollection<IAthlete>();
         
            return cloned;
        }
    }
}
