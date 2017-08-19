using System;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Adre.Controls.RankingList
{
    class ItemViewModel : BindableBase, IItemViewModel
    {
        int _no;

        float _finalScore;

        string _medal;

        IAthlete _athlete;

        IContingent _contingent;

        IEvent _event;
        
        public Guid Id { get; set; }

        public int No { get => _no; set => SetProperty(ref _no, value); }

        public IContingent Contingent { get => _contingent; set => SetProperty(ref _contingent, value); }

        public IAthlete Athlete { get => _athlete; set => SetProperty(ref _athlete, value); }

        public float FinalScore { get => _finalScore; set => SetProperty(ref _finalScore, value); }

        public IEvent Event { get => _event; set => SetProperty(ref _event, value); }

        public ObservableCollection<string> MedalList => new ObservableCollection<string> { "", "Gold", "Silver", "Bronze" };

        public string SelectedMedal {
            get => _medal;
            set => SetProperty(ref _medal, value);
        }

        public string EventName { get => _event.Name; }
    }
}
