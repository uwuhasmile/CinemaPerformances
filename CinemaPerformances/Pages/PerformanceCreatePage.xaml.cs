using CinemaPerformances.ViewModels;

namespace CinemaPerformances.Pages;

public partial class PerformanceCreatePage : ContentPage
{
    private readonly PerformanceCreateViewModel _viewModel;
	public PerformanceCreatePage(PerformanceCreateViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
	}
}