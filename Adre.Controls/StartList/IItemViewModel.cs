using Adre.Controls.StartList.Team;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Adre.Controls.StartList
{
    public interface IItemViewModel
    {
        event AthleteChangedHandler OnAthleteChanged;

        Guid Id { get; set; }

        int No { get; set; }

        DateTime DateTimeStart { get; set; }
        
        IEvent SelectedEvent { get; set; }

        string Arena { get; set; }

        string Venue { get; set; }

        string  Remarks { get; set; }
        
        ObservableCollection<IAthlete> AthleteList { get; set; }

        IAthlete SelectedAthlete { get; set; }
        
        IPhase SelectedPhase { get; set; }

        object Clone();

        ObservableCollection<IEvent> IEventList { get; set; }

        ObservableCollection<IPhase> IPhaseList { get; set; }

        ICollection<IEvent> ReservedEventList { get; set; }

        ICollection<string> GenderList { get; set; }

        string SelectedGender { get; set; }
    }
}
