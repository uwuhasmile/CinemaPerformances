using System.Collections.ObjectModel;

using CinemaPerformances.Services;
using CinemaPerformances.UIModels;

namespace CinemaPerformances.Pages;

[QueryProperty(nameof(CurrentCinemaHall), nameof(CurrentCinemaHall))]
public partial class CinemaHallDetailsPage : ContentPage
{
    public CinemaHallUIModel CurrentCinemaHall
    {
        get;
        set
        {
            field = value;
            field.LoadPerformances();
            BindingContext = CurrentCinemaHall;
        }
    }
    public ObservableCollection<PerformanceUIModel> Performances { get; set; }

    private readonly IStorageService _storageService;

    public CinemaHallDetailsPage(IStorageService storageService)
	{
		InitializeComponent();
        _storageService = storageService;
    }

    private async void PerformanceSelected(object sender, SelectionChangedEventArgs e)
    {
        var performance = (PerformanceUIModel)e.CurrentSelection[0];
        await Shell.Current.GoToAsync($"{nameof(PerformanceDetailsPage)}",
                new Dictionary<string, object> { { nameof(PerformanceDetailsPage.CurrentPerformance), performance } });
    }
}