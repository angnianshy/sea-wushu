
using System;
using System.Collections.ObjectModel;

namespace Adre.Controls.RankingList
{
    public interface IItemViewModel
    {
        Guid Id { get; set; }

        int No { get; set; }

        float FinalScore { get; set; }

        IContingent Contingent { get; set; }

        IAthlete Athlete { get; set; }

        IEvent Event { get; set; }

        string SelectedMedal { get; set; }
        
        string EventName { get; }

        ObservableCollection<string> MedalList { get; }

    }
}
