using CinemaPerformances.ViewModels;

namespace CinemaPerformances.Pages;

public partial class CinemaHallsPage : ContentPage
{
    public CinemaHallsPage(CinemaHallsViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
	}
}