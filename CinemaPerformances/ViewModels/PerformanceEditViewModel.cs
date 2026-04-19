using System.Collections.ObjectModel;

using CinemaPerformances.Common;
using CinemaPerformances.DTOModels;
using CinemaPerformances.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CinemaPerformances.ViewModels;

public partial class PerformanceEditViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IPerformanceService _performanceService;

    private Guid _performanceId;
    private Guid _cinemaHallId;
    private EnumWithName<MovieGenre>[] _genres;

    [ObservableProperty]
    private PerformanceListDTO? _currentPerformance;
    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private EnumWithName<MovieGenre>? _genre;
    [ObservableProperty]
    DateTime _releaseDate;
    [ObservableProperty]
    TimeSpan? _start;
    [ObservableProperty]
    double _duration;

    [ObservableProperty]
    private Dictionary<string, string> _errors;

    public EnumWithName<MovieGenre>[] Genres => _genres;

    public PerformanceEditViewModel(IPerformanceService performanceService)
    {
        _performanceService = performanceService;
        _genres = EnumExtensions.GetValueWithNames<MovieGenre>();
        Errors = InitErrors();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _performanceId = (Guid)query["PerformanceId"];
        _cinemaHallId = (Guid)query["CinemaHallId"];
    }

    [RelayCommand]
    public async Task RefreshData()
    {
        IsBusy = true;
        try
        {
            var performance = await _performanceService.GetPerformance(_performanceId)
                ?? throw new Exception("Performance doesn't exist");
            Name = performance.Name;
            Genre = Genres.FirstOrDefault(x => x.Value == performance.Genre);
            ReleaseDate = performance.ReleaseDate;
            Start = performance.Start.TimeOfDay;
            Duration = performance.Duration;
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

    [RelayCommand]
    public async Task UpdatePerformance()
    {
        IsBusy = true;
        var errors = Validation.ValidatePerformance(Name, Genre?.Value, ReleaseDate, Start!.Value, Duration);
        Errors = InitErrors();
        if (errors.Count > 0)
        {
            foreach (var error in errors)
            {
                if (string.IsNullOrWhiteSpace(Errors[error.MemberName]))
                {
                    Errors[error.MemberName] = error.Message;
                    continue;
                }
                Errors[error.MemberName] += Environment.NewLine + error.Message;
            }
            OnPropertyChanged(nameof(Errors));
            IsBusy = false;
            return;
        }

        try
        {
            var updatedPerformance = new PerformanceEditDTO(_performanceId, _cinemaHallId, Name, Genre!.Value, ReleaseDate, Start!.Value, Duration);
            await _performanceService.EditPerformance(updatedPerformance);
            await Shell.Current.DisplayAlertAsync("Success", "Performance updated successfully!", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to updated performance: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task Back()
    {
        try
        {
            IsBusy = true;
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate back: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private Dictionary<string, string> InitErrors()
    {
        return new Dictionary<string, string>()
            {
                { nameof(Name), string.Empty },
                { nameof(Genre), string.Empty },
                { nameof(ReleaseDate), string.Empty },
                { nameof(Start), string.Empty },
                { nameof(Duration), string.Empty },
            };
    }
}
