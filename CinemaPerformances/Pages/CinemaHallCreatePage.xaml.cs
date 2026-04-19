using CinemaPerformances.ViewModels;

namespace CinemaPerformances.Pages;

public partial class CinemaHallCreatePage : ContentPage
{
    private readonly CinemaHallCreateViewModel _viewModel;
	public CinemaHallCreatePage(CinemaHallCreateViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
	}
}