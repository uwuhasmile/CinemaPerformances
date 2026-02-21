using CinemaPerformances.DBModels;

namespace CinemaPerformances.Services;

public class StorageService
{
    private List<CinemaHallDBModel>? _cinemaHalls;
    private List<PerformanceDBModel>? _performances;

    private void LoadData()
    {
        if (_cinemaHalls == null)
            _cinemaHalls = FakeStorage.CinemaHalls.ToList();
        if (_performances == null)
            _performances = FakeStorage.Performances.ToList();
    }

    public IEnumerable<PerformanceDBModel> GetPerformances(Guid cinemaHallId)
    {
        LoadData();
        List<PerformanceDBModel> result = new();
        foreach (var performance in _performances ?? [])
            if (performance.CinemaHallId == cinemaHallId)
                result.Add(performance);
        return result;
    }

    public IEnumerable<CinemaHallDBModel> GetAllCinemaHalls()
    {
        LoadData();
        return [.. _cinemaHalls ?? []];
    }
}