using CinemaPerformances.Services;
using CinemaPerformances.UIModels;

using System.Collections.ObjectModel;

namespace CinemaPerformances.Pages;

public partial class CinemaHallsPage : ContentPage
{
    private readonly IStorageService _storageService;
    public ObservableCollection<CinemaHallUIModel> CinemaHalls { get; set; }

    public CinemaHallsPage(IStorageService storageService)
	{
        InitializeComponent();

        _storageService = storageService;
        CinemaHalls = new();
        foreach (var cinemaHall in _storageService.GetAllCinemaHalls())
            CinemaHalls.Add(new(storageService, cinemaHall));
        
        BindingContext = this;
	}

    public async void CinemaHallSelected(object sender, SelectionChangedEventArgs e)
    {
        var cinemaHall = (CinemaHallUIModel)e.CurrentSelection[0];
    }
}