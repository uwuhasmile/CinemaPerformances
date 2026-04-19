using CinemaPerformances.ViewModels;

namespace CinemaPerformances.Pages;

public partial class CinemaHallEditPage : ContentPage
{
    private readonly CinemaHallEditViewModel _viewModel;
	public CinemaHallEditPage(CinemaHallEditViewModel viewModel)
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