using CinemaPerformances.Pages;
using CinemaPerformances.Repositories;
using CinemaPerformances.Services;
using CinemaPerformances.Storage;
using CinemaPerformances.ViewModels;

using Microsoft.Extensions.Logging;

namespace CinemaPerformances;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<IStorageContext, FileStorageContext>();
        builder.Services.AddSingleton<ICinemaHallRepository, CinemaHallRepository>();
        builder.Services.AddSingleton<IPerformanceRepository, PerformanceRepository>();

        builder.Services.AddSingleton<ICinemaHallService, CinemaHallService>();
        builder.Services.AddSingleton<IPerformanceService, PerformanceService>();

        builder.Services.AddSingleton<CinemaHallsPage>();
        builder.Services.AddSingleton<CinemaHallCreatePage>();
        builder.Services.AddSingleton<CinemaHallEditPage>();
        builder.Services.AddTransient<CinemaHallDetailsPage>();
        builder.Services.AddTransient<PerformanceCreatePage>();
        builder.Services.AddTransient<PerformanceEditPage>();
        builder.Services.AddTransient<PerformanceDetailsPage>();

        builder.Services.AddSingleton<CinemaHallsViewModel>();
        builder.Services.AddSingleton<CinemaHallCreateViewModel>();
        builder.Services.AddSingleton<CinemaHallEditViewModel>();
        builder.Services.AddTransient<CinemaHallDetailsViewModel>();
        builder.Services.AddTransient<PerformanceCreateViewModel>();
        builder.Services.AddTransient<PerformanceEditViewModel>();
        builder.Services.AddTransient<PerformanceDetailsViewModel>();

        return builder.Build();
    }
}
