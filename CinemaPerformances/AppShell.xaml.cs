using CinemaPerformances.Pages;

namespace CinemaPerformances;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute($"{nameof(CinemaHallsPage)}/{nameof(CinemaHallDetailsPage)}", typeof(CinemaHallDetailsPage));
        Routing.RegisterRoute($"{nameof(CinemaHallsPage)}/{nameof(CinemaHallDetailsPage)}/{nameof(PerformanceDetailsPage)}", typeof(PerformanceDetailsPage));
    }
}
