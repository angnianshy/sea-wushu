
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace Adre.Controls.StartList
{
    public class BaseItemModel : BindableBase
    {
        Guid _id;

        int _no;

        DateTime _dateTimeStart;
        
        protected IEvent _selectedEvent;

        ObservableCollection<IEvent> _eventList;

        string _arena, _venue, _remarks;

        ObservableCollection<IPhase> _phaseList;

        IPhase _selectedPhase;

        public BaseItemModel()
        {
            DateTimeStart = DateTime.Now;
        }

        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public int No
        {
            get => _no;
            set => SetProperty(ref _no, value); 
        }

        public DateTime DateTimeStart
        {
            get => _dateTimeStart;
            set => SetProperty(ref _dateTimeStart, value); 
        }

        public virtual IEvent SelectedEvent
        {
            get => _selectedEvent;
            set => SetProperty(ref _selectedEvent, value); 
        }

        public ObservableCollection<IEvent> EventList
        {
            get => _eventList;
            set => SetProperty(ref _eventList, value); 
        }

        public string Arena
        {
            get => _arena;
            set => SetProperty(ref _arena, value); 
        }

        public string Venue
        {
            get => _venue;
            set => SetProperty(ref _venue, value);
        }

        public string Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }
        public ObservableCollection<IPhase> PhaseList
        {
            get => _phaseList;
            set => SetProperty(ref _phaseList, value);
        }

        public IPhase SelectedPhase
        {
            get => _selectedPhase;
            set => SetProperty(ref _selectedPhase, value);
        }
    }
}
