using System.Collections.ObjectModel;

using CinemaPerformances.DTOModels;
using CinemaPerformances.Pages;
using CinemaPerformances.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CinemaPerformances.ViewModels;

public partial class CinemaHallDetailsViewModel : ObservableObject, IQueryAttributable
{
    private readonly ICinemaHallService _cinemaHallService;
    private readonly IPerformanceService _performanceService;

    [ObservableProperty]
    private CinemaHallListDTO? _currentCinemaHall;
    [ObservableProperty]
    private ObservableCollection<PerformanceListDTO>? _performances;


    public CinemaHallDetailsViewModel(ICinemaHallService cinemaHallService, IPerformanceService performanceService)
    {
        _cinemaHallService = cinemaHallService;
        _performanceService = performanceService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var id = (Guid)query["CinemaHallId"];
        CurrentCinemaHall = _cinemaHallService.GetCinemaHall(id);
        Performances = new(_performanceService.GetPerformancesByCinemaHall(id));
        OnPropertyChanged(nameof(CurrentCinemaHall));
        OnPropertyChanged(nameof(Performances));
    }

    [RelayCommand]
    private void LoadPerformance(Guid id)
    {
        Shell.Current.GoToAsync($"{nameof(PerformanceDetailsPage)}", new Dictionary<string, object> { { "PerformanceId", id } });
    }
}
