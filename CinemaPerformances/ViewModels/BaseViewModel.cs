using CommunityToolkit.Mvvm.ComponentModel;

namespace CinemaPerformances.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy = false;

    public bool IsNotBusy => !IsBusy;
}
