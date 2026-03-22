using System.Collections.ObjectModel;

using CinemaPerformances.DTOModels;
using CinemaPerformances.Pages;
using CinemaPerformances.Services;

namespace CinemaPerformances.ViewModels;

public class CinemaHallsViewModel
{
    public ObservableCollection<CinemaHallListDTO> CinemaHalls { get; set; }
    public CinemaHallListDTO? SelectedCinemaHall { get; set; }
    public Command CinemaHallSelectedCommand { get; }

    private readonly ICinemaHallService _cinemaHallService;

    public CinemaHallsViewModel(ICinemaHallService cinemaHallService)
    {
        _cinemaHallService = cinemaHallService;

        CinemaHalls = new(_cinemaHallService.GetCinemaHalls());
        CinemaHallSelectedCommand = new(LoadCinemaHall);
    }

    public void LoadCinemaHall()
    {
        if (SelectedCinemaHall is null)
            return;
        Shell.Current.GoToAsync($"{nameof(CinemaHallDetailsPage)}", new Dictionary<string, object> { { "CinemaHallId", SelectedCinemaHall?.Id! } });
    }
}
