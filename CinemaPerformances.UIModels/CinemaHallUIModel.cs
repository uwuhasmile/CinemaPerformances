using CinemaPerformances.Common;
using CinemaPerformances.DBModels;
using CinemaPerformances.Services;

namespace CinemaPerformances.UIModels;

public class CinemaHallUIModel
{
    private readonly IStorageService _storageService;

    private CinemaHallDBModel? _dbModel;
    private List<PerformanceUIModel>? _performances;

    public Guid? Id => _dbModel?.Id;
    public string? Name { get; set; }
    public int Seats { get; set; }
    public CinemaHallType Type { get; set; }
    public IReadOnlyList<PerformanceUIModel> Performances => _performances ?? [];
    public double TotalDuration { get; private set; }

    public string TotalDurationDescription => _performances is null ? "Not loaded" : $"{TotalDuration} minutes";

    public CinemaHallUIModel(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public CinemaHallUIModel(IStorageService storageService, CinemaHallDBModel dbModel) : this(storageService)
    {
        _dbModel = dbModel;
        Name = dbModel.Name;
        Seats = dbModel.Seats;
        Type = dbModel.Type;
    }

    public void Save()
    {
        if (_dbModel is not null)
        {
            _dbModel.Name = Name;
            _dbModel.Seats = Seats;
            _dbModel.Type = Type;
        }
        else
            _dbModel = new(Name, Seats, Type);
    }

    public void LoadPerformances()
    {
        if (Id is null || _performances is not null)
            return;

        _performances = new();
        foreach (var performance in _storageService.GetPerformances(Id.Value))
        {
            _performances.Add(new(performance));
            TotalDuration += performance.Duration;
        }
    }

    public override string ToString() => $"\"{Name}\": ${Type}, {Seats} seats";
}