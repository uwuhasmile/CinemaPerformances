using CinemaPerformances.ViewModels;

namespace CinemaPerformances.Pages;

public partial class CinemaHallDetailsPage : ContentPage
{
    public CinemaHallDetailsPage(CinemaHallDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}