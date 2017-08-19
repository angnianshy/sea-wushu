using System;
using System.ComponentModel;
using Prism.Mvvm;

namespace Adre.Controls.ResultList.Team
{
  class ItemViewModel : BindableBase, IItemViewModel
  {
    StartList.IItemViewModel _startListItem;

    float _qm, _ob, _dd, _penalty, _finalScore;

    string _remarks, _athlete, _medal;

    public Guid Id { get; set; }

    public StartList.IItemViewModel StartListItem
    {
      get => _startListItem;
      set
      {
        if (_startListItem != null) ((BindableBase)_startListItem).PropertyChanged -= _OnStartListItemChanged;
        SetProperty(ref _startListItem, value);
        if (_startListItem != null) ((BindableBase)_startListItem).PropertyChanged += _OnStartListItemChanged;
      }
    }

    public string Remarks { get => _remarks; set => SetProperty(ref _remarks, value); }
    public string Medal { get => _medal; set => SetProperty(ref _medal, value); }

    public float QM { get => _qm; set => SetProperty(ref _qm, value); }

    public float OB { get => _ob; set => SetProperty(ref _ob, value); }

    public float DD { get => _dd; set => SetProperty(ref _dd, value); }

    public float Penalty { get => _penalty; set => SetProperty(ref _penalty, value); }

    public float FinalScore { get => _finalScore; set => SetProperty(ref _finalScore, value); }

    public string Athlete { get => _athlete; set => SetProperty(ref _athlete, value); }

    protected override void OnPropertyChanged(PropertyChangedEventArgs args)
    {
      if (args.PropertyName != "FinalScore" &&
          args.PropertyName != "Remarks" &&
          args.PropertyName != "Medal" &&
          args.PropertyName != "StartListITem")
        FinalScore = QM + OB + DD - Penalty;

      base.OnPropertyChanged(args);
    }

    void _OnStartListItemChanged(object sender, PropertyChangedEventArgs args)
    {
      if (args.PropertyName == "SelectedAthlete")
      {
        Athlete = StartListItem.SelectedAthlete?.ToString();
      }

    }
  }

}
