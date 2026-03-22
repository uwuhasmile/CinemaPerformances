using CinemaPerformances.Common;
using CinemaPerformances.DTOModels;
using CinemaPerformances.Services;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CinemaPerformances.ViewModels;

public partial class PerformanceDetailsViewModel : ObservableObject, IQueryAttributable
{
    private readonly IPerformanceService _performanceService;

    private PerformanceDetailsDTO? _currentPerformance;

    public string? Name => _currentPerformance?.Name;
    public MovieGenre? Genre => _currentPerformance?.Genre;
    public DateTime? ReleaseDate => _currentPerformance?.ReleaseDate;
    public DateTime? Start => _currentPerformance?.Start;
    public double? Duration => _currentPerformance?.Duration;
    public DateTime? End => _currentPerformance?.End;


    public PerformanceDetailsViewModel(IPerformanceService performanceService)
    {
        _performanceService = performanceService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var id = (Guid)query["PerformanceId"];
        _currentPerformance = _performanceService.GetPerformance(id);
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Genre));
        OnPropertyChanged(nameof(ReleaseDate));
        OnPropertyChanged(nameof(Start));
        OnPropertyChanged(nameof(Duration));
        OnPropertyChanged(nameof(End));
    }
}
