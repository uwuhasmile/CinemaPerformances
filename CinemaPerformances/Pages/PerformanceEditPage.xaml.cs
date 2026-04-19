using CinemaPerformances.ViewModels;

namespace CinemaPerformances.Pages;

public partial class PerformanceEditPage : ContentPage
{
    private readonly PerformanceEditViewModel _viewModel;
	public PerformanceEditPage(PerformanceEditViewModel viewModel)
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