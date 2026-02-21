using CinemaPerformances.Common;
using CinemaPerformances.DBModels;

namespace CinemaPerformances.UIModels;

public class PerformanceUIModel
{
    private PerformanceDBModel? _dbModel;

    public Guid Id => _dbModel?.Id ?? Guid.Empty;
    public readonly Guid CinemaHallId;
    public string? Name { get; set; }
    public MovieGenre Genre { get; set; }
    public DateTime ReleaseDate
    {
        get;
        set
        {
            if (_dbModel is not null)
                return;
            field = value;
        }
    }
    public DateTime Start {
        get;
        set
        {
            field = value;
            CalculateEndTime();
        }
    }
    public double Duration {
        get;
        set
        {
            field = value;
            CalculateEndTime();
        }
    }
    public DateTime End { get; private set; }

    public PerformanceUIModel(Guid cinemaHallId)
    {
        CinemaHallId = cinemaHallId;
    }

    public PerformanceUIModel(PerformanceDBModel dbModel)
    {
        _dbModel = dbModel;
        CinemaHallId = _dbModel.CinemaHallId;
        Name = dbModel.Name;
        Genre = dbModel.Genre;
        ReleaseDate = dbModel.ReleaseDate;
        Start = dbModel.Start;
        Duration = dbModel.Duration;
    }

    private void CalculateEndTime()
    {
        End = Start.AddMinutes(Duration);
    }

    public void Save()
    {
        if (_dbModel is not null)
        {
            _dbModel.Name = Name;
            _dbModel.Genre = Genre;
            _dbModel.Start = Start;
            _dbModel.Duration = Duration;
        }
        else
            _dbModel = new(CinemaHallId, Name, Genre, ReleaseDate, Start, Duration);
    }
}
