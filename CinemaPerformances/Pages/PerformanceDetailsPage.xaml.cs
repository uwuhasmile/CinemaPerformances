using CinemaPerformances.ViewModels;

namespace CinemaPerformances.Pages;

public partial class PerformanceDetailsPage : ContentPage
{
	public PerformanceDetailsPage(PerformanceDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}