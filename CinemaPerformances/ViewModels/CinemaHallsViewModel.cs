using System.Collections.ObjectModel;

using CinemaPerformances.Common;
using CinemaPerformances.Common.Enums;
using CinemaPerformances.DTOModels;
using CinemaPerformances.Pages;
using CinemaPerformances.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CinemaPerformances.ViewModels;

public partial class CinemaHallsViewModel : BaseViewModel
{
    private readonly ICinemaHallService _cinemaHallService;

    private EnumWithName<CinemaHallFilter>[] _filters;
    private EnumWithName<CinemaHallSorting>[] _sortings;

    [ObservableProperty]
    private ObservableCollection<CinemaHallListDTO> _cinemaHalls;
    [ObservableProperty]
    private CinemaHallListDTO? _selectedCinemaHall;

    [ObservableProperty]
    private string? _searchQuery;
    [ObservableProperty]
    EnumWithName<CinemaHallFilter> _filter;
    [ObservableProperty]
    EnumWithName<CinemaHallSorting> _sorting;

    public EnumWithName<CinemaHallFilter>[] Filters => _filters;
    public EnumWithName<CinemaHallSorting>[] Sortings => _sortings;

    public CinemaHallsViewModel(ICinemaHallService cinemaHallService)
    {
        _cinemaHallService = cinemaHallService;
        _filters = EnumExtensions.GetValueWithNames<CinemaHallFilter>();
        _sortings = EnumExtensions.GetValueWithNames<CinemaHallSorting>();
        _filter = EnumExtensions.GetEnumWithName(CinemaHallFilter.None);
        _sorting = EnumExtensions.GetEnumWithName(CinemaHallSorting.None);
    }

    partial void OnFilterChanged(EnumWithName<CinemaHallFilter> value) => Task.Run(RefreshData);
    partial void OnSortingChanged(EnumWithName<CinemaHallSorting> value) => Task.Run(RefreshData);

    [RelayCommand]
    public async Task RefreshData()
    {
        IsBusy = true;
        try
        {
            CinemaHalls = new();
            await foreach (var department in _cinemaHallService.GetCinemaHalls(SearchQuery, Filter.Value, Sorting.Value))
                CinemaHalls.Add(department);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to load cinema halls: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task LoadCinemaHall()
    {
        IsBusy = true;
        try
        {
            if (SelectedCinemaHall is null) return;
            await Shell.Current.GoToAsync($"{nameof(CinemaHallDetailsPage)}", new Dictionary<string, object> { { "CinemaHallId", SelectedCinemaHall?.Id! } });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to cinema hall details: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task AddCinemaHall()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync($"{nameof(CinemaHallCreatePage)}");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to cinema hall create page: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task EditCinemaHall(CinemaHallListDTO cinemaHall)
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync($"{nameof(CinemaHallEditPage)}", new Dictionary<string, object> { { "CinemaHallId", cinemaHall.Id } });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to cinema hall edit page: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task DeleteCinemaHall(CinemaHallListDTO cinemaHall)
    {
        IsBusy = true;
        try
        {
            if (await Shell.Current.DisplayAlertAsync("Confirm", "Are you sure you want to delete this cinema hall?", "Yes", "No"))
                await _cinemaHallService.DeleteCinemaHall(cinemaHall.Id);
            CinemaHalls?.Remove(cinemaHall);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to delete cinema hall: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
