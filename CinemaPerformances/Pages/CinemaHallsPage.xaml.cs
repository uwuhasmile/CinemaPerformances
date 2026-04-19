using CinemaPerformances.ViewModels;

namespace CinemaPerformances.Pages;

public partial class CinemaHallsPage : ContentPage
{
    private readonly CinemaHallsViewModel _viewModel;

    public CinemaHallsPage(CinemaHallsViewModel viewModel)
	{
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        await _viewModel.RefreshData();
    }
}