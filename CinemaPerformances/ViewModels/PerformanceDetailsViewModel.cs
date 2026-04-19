using CinemaPerformances.Common;
using CinemaPerformances.DTOModels;
using CinemaPerformances.Services;

namespace CinemaPerformances.ViewModels;

public partial class PerformanceDetailsViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IPerformanceService _performanceService;

    private PerformanceDetailsDTO? _currentPerformance;

    private Guid _performanceId;

    public string? Name => _currentPerformance?.Name;
    public MovieGenre? Genre => _currentPerformance?.Genre;
    public DateTime? ReleaseDate => _currentPerformance?.ReleaseDate;
    public TimeSpan? Start => _currentPerformance?.Start.TimeOfDay;
    public double? Duration => _currentPerformance?.Duration;
    public TimeSpan? End { get; private set; }


    public PerformanceDetailsViewModel(IPerformanceService performanceService)
    {
        _performanceService = performanceService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _performanceId = (Guid)query["PerformanceId"];
    }

    internal async Task RefreshData()
    {
        IsBusy = true;
        try
        {
            _currentPerformance = await _performanceService.GetPerformance(_performanceId) ?? throw new Exception("Performance does not exist.");
            CalculateEnd();
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Genre));
            OnPropertyChanged(nameof(ReleaseDate));
            OnPropertyChanged(nameof(Start));
            OnPropertyChanged(nameof(Duration));
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to load performance details: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void CalculateEnd()
    {
        if (Start is null || Duration is null) return;

        var start = Start.Value;
        var duration = Duration.Value;
        End = start.Add(new(0, (int)duration, 0));
    }
}
