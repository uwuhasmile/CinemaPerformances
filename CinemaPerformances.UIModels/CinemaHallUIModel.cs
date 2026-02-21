using CinemaPerformances.Common;
using CinemaPerformances.DBModels;
using CinemaPerformances.Services;

namespace CinemaPerformances.UIModels;

public class CinemaHallUIModel
{
    private CinemaHallDBModel? _dbModel;
    private readonly List<PerformanceUIModel> _performances;

    public Guid? Id => _dbModel?.Id;
    public string? Name { get; set; }
    public int Seats { get; set; }
    public CinemaHallType Type { get; set; }
    public IReadOnlyList<PerformanceUIModel> Performances => _performances;
    public double TotalDuration { get; private set; }

    public CinemaHallUIModel()
    {
        _performances = new();
    }

    public CinemaHallUIModel(CinemaHallDBModel dbModel) : this()
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

    public void LoadPerformances(StorageService storage)
    {
        if (Id is null || _performances.Count > 0)
            return;
        
        foreach (var performance in storage.GetPerformances(Id.Value))
        {
            _performances.Add(new(performance));
            TotalDuration += performance.Duration;
        }
    }
}