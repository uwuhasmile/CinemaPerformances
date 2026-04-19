using System.Collections.ObjectModel;

using CinemaPerformances.Common;
using CinemaPerformances.Common.Enums;
using CinemaPerformances.DTOModels;
using CinemaPerformances.Pages;
using CinemaPerformances.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CinemaPerformances.ViewModels;

public partial class CinemaHallDetailsViewModel : BaseViewModel, IQueryAttributable
{
    private readonly ICinemaHallService _cinemaHallService;
    private readonly IPerformanceService _performanceService;

    private Guid _cinamaHallId;
    private EnumWithName<PerformanceFilter>[] _filters;
    private EnumWithName<PerformanceSorting>[] _sortings;

    [ObservableProperty]
    private CinemaHallListDTO? _currentCinemaHall;
    [ObservableProperty]
    private ObservableCollection<PerformanceListDTO>? _performances;

    [ObservableProperty]
    private string? _searchQuery;
    [ObservableProperty]
    EnumWithName<PerformanceFilter> _filter;
    [ObservableProperty]
    EnumWithName<PerformanceSorting> _sorting;

    public EnumWithName<PerformanceFilter>[] Filters => _filters;
    public EnumWithName<PerformanceSorting>[] Sortings => _sortings;

    public CinemaHallDetailsViewModel(ICinemaHallService cinemaHallService, IPerformanceService performanceService)
    {
        _cinemaHallService = cinemaHallService;
        _performanceService = performanceService;
        _filters = EnumExtensions.GetValueWithNames<PerformanceFilter>();
        _sortings = EnumExtensions.GetValueWithNames<PerformanceSorting>();
        _filter = EnumExtensions.GetEnumWithName(PerformanceFilter.None);
        _sorting = EnumExtensions.GetEnumWithName(PerformanceSorting.None);
    }

    partial void OnFilterChanged(EnumWithName<PerformanceFilter> value) => Task.Run(RefreshData);
    partial void OnSortingChanged(EnumWithName<PerformanceSorting> value) => Task.Run(RefreshData);

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _cinamaHallId = (Guid)query["CinemaHallId"];
        OnPropertyChanged(nameof(Performances));
    }

    [RelayCommand]
    public async Task RefreshData()
    {
        IsBusy = true;
        try
        {
            CurrentCinemaHall = await _cinemaHallService.GetCinemaHall(_cinamaHallId) ?? throw new Exception("Cinema hall doesn't exist");
            Performances = new ObservableCollection<PerformanceListDTO>(
                await _performanceService.GetPerformancesByCinemaHall(_cinamaHallId, SearchQuery, Filter.Value, Sorting.Value));
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to load cinema hall details: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task LoadPerformance(Guid id)
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync($"{nameof(PerformanceDetailsPage)}", new Dictionary<string, object> { { "PerformanceId", id } });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to performanced details: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task AddPerformance()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync($"{nameof(PerformanceCreatePage)}", new Dictionary<string, object> { { "CinemaHallId", _cinamaHallId } });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to performance create page: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task EditPerformance(PerformanceListDTO performance)
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync($"{nameof(PerformanceEditPage)}", new Dictionary<string, object> { { "PerformanceId", performance.Id }, { "CinemaHallId", _cinamaHallId } });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to performance edit page: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task DeletePerformance(PerformanceListDTO performance)
    {
        IsBusy = true;
        try
        {
            if (await Shell.Current.DisplayAlertAsync("Confirm", "Are you sure you want to delete this performance?", "Yes", "No"))
                await _performanceService.DeletePerformance(performance.Id);
            Performances?.Remove(performance);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to delete performance: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
