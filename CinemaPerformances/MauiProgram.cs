using CinemaPerformances.Pages;
using CinemaPerformances.Services;

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
        builder.Services.AddSingleton<IStorageService, StorageService>();
        builder.Services.AddTransient<CinemaHallDetailsPage>();
        builder.Services.AddTransient<PerformanceDetailsPage>();

        return builder.Build();
    }
}
