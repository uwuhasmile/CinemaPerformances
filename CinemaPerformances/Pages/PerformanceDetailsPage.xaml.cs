using CinemaPerformances.UIModels;

namespace CinemaPerformances.Pages;

[QueryProperty(nameof(CurrentPerformance), nameof(CurrentPerformance))]
public partial class PerformanceDetailsPage : ContentPage
{
    public PerformanceUIModel CurrentPerformance
    {
        get;
        set
        {
            field = value;
            BindingContext = field;
        }
    }

	public PerformanceDetailsPage()
	{
		InitializeComponent();
	}
}