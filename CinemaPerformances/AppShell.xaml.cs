using CinemaPerformances.Pages;

namespace CinemaPerformances;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute($"{nameof(CinemaHallsPage)}/{nameof(CinemaHallCreatePage)}", typeof(CinemaHallCreatePage));
        Routing.RegisterRoute($"{nameof(CinemaHallsPage)}/{nameof(CinemaHallEditPage)}", typeof(CinemaHallEditPage));
        Routing.RegisterRoute($"{nameof(CinemaHallsPage)}/{nameof(CinemaHallDetailsPage)}", typeof(CinemaHallDetailsPage));
        Routing.RegisterRoute($"{nameof(CinemaHallsPage)}/{nameof(PerformanceCreatePage)}", typeof(PerformanceCreatePage));
        Routing.RegisterRoute($"{nameof(CinemaHallsPage)}/{nameof(PerformanceEditPage)}", typeof(PerformanceEditPage));
        Routing.RegisterRoute($"{nameof(CinemaHallsPage)}/{nameof(CinemaHallDetailsPage)}/{nameof(PerformanceDetailsPage)}", typeof(PerformanceDetailsPage));
    }
}
