using CinemaPerformances.Common;
using CinemaPerformances.DTOModels;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CinemaPerformances.Services;

public static class Validation
{
    public record struct ValidationError(string Message, string MemberName);

    public static List<ValidationError> Validate(this PerformanceCreateDTO performance)
    {
        List<ValidationError> errors = new();
        if (performance.CinemaHallId == Guid.Empty)
            errors.Add(new("Performance must have the cinema hall defined", nameof(performance.CinemaHallId)));
        errors.AddRange(ValidatePerformance(performance.Name, performance.Genre, performance.ReleaseDate, performance.Start, performance.Duration));
        return errors;
    }

    public static List<ValidationError> Validate(this PerformanceEditDTO performance)
    {
        List<ValidationError> errors = new();
        if (performance.Id == Guid.Empty)
            errors.Add(new("Invalid performance id to edit", nameof(performance.Id)));
        if (performance.CinemaHallId == Guid.Empty)
            errors.Add(new("Performance must have the cinema hall defined", nameof(performance.CinemaHallId)));
        errors.AddRange(ValidatePerformance(performance.Name, performance.Genre, performance.ReleaseDate, performance.Start, performance.Duration));
        return errors;
    }

    public static List<ValidationError> ValidatePerformance(string name, MovieGenre? genre, DateTime releaseDate, TimeSpan? start, double duration)
    {
        List<ValidationError> errors = new();
        if (string.IsNullOrWhiteSpace(name)) errors.Add(new("Performance must have a name", "Name"));
        if (genre is null) errors.Add(new("Performance must have a genre picked", "Genre"));
        if (start is null) errors.Add(new("Performance must have a defined start time", "Start"));
        if (duration <= 0.0) errors.Add(new("Performance duration must be positive", "Duration"));
        return errors;
    }

    public static List<ValidationError> Validate(this CinemaHallCreateDTO cinemaHall)
    {
        return ValidateCinemaHall(cinemaHall.Name, cinemaHall.Seats, cinemaHall.Type);
    }

    public static List<ValidationError> Validate(this CinemaHallEditDTO cinemaHall)
    {
        List<ValidationError> errors = new();
        if (cinemaHall.Id == Guid.Empty) errors.Add(new("Invalid cinema hall id to edit", nameof(cinemaHall.Id)));
        errors.AddRange(ValidateCinemaHall(cinemaHall.Name, cinemaHall.Seats, cinemaHall.Type));
        return errors;
    }

    public static List<ValidationError> ValidateCinemaHall(string name, int seats, CinemaHallType? type)
    {
        List<ValidationError> errors = new();
        if (string.IsNullOrWhiteSpace(name)) errors.Add(new("Cinema hall must have a name", "Name"));
        if (seats <= 0) errors.Add(new("Cinema hall can't have a non positive amount of seats", "Seats"));
        if (type is null) errors.Add(new("Cinema hall type must be specified", "Type"));
        return errors;
    }
}
