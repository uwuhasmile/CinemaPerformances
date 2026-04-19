using CinemaPerformances.Common;
using CinemaPerformances.DTOModels;
using CinemaPerformances.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CinemaPerformances.ViewModels;

public partial class CinemaHallEditViewModel : BaseViewModel, IQueryAttributable
{
    private readonly ICinemaHallService _cinemaHallService;

    private Guid _cinemaHallId;
    private EnumWithName<CinemaHallType>[] _types;

    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private int _seats;
    [ObservableProperty]
    private EnumWithName<CinemaHallType>? _type;

    [ObservableProperty]
    private Dictionary<string, string> _errors;

    public EnumWithName<CinemaHallType>[] Types => _types;

    public CinemaHallEditViewModel(ICinemaHallService cinemaHallService)
    {
        _cinemaHallService = cinemaHallService;
        _types = EnumExtensions.GetValueWithNames<CinemaHallType>();
        Errors = InitErrors();
        _seats = 1;
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _cinemaHallId = (Guid)query["CinemaHallId"];
    }

    [RelayCommand]
    public async Task RefreshData()
    {
        IsBusy = true;
        try
        {
            var cinemaHall = await _cinemaHallService.GetCinemaHall(_cinemaHallId)
                ?? throw new Exception("Cinema hall doesn't exist.");

            Name = cinemaHall.Name;
            Seats = cinemaHall.Seats;
            Type = Types.FirstOrDefault(t => t.Value == cinemaHall.Type);
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
    public async Task UpdateCinemaHall()
    {
        IsBusy = true;
        var errors = Validation.ValidateCinemaHall(Name, Seats, Type?.Value);
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
            var updatedCinemaHall = new CinemaHallEditDTO(_cinemaHallId, Name, Seats, Type!.Value);
            await _cinemaHallService.EditCinemaHall(updatedCinemaHall);
            await Shell.Current.DisplayAlertAsync("Success", "Cinema hall updated successfully!", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to update cinema hall: {ex.Message}", "OK");
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
                { nameof(Seats), string.Empty },
                { nameof(Type), string.Empty },
            };
    }
}
