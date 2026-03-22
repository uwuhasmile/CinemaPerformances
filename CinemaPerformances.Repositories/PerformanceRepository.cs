using CinemaPerformances.DBModels;
using CinemaPerformances.Storage;

namespace CinemaPerformances.Repositories;

public class PerformanceRepository : IPerformanceRepository
{
    private readonly IStorageContext _storage;

    public PerformanceRepository(IStorageContext storage)
    {
        _storage = storage;
    }

    public PerformanceDBModel? GetPerformance(Guid id) => _storage.GetPerformance(id);

    public IEnumerable<PerformanceDBModel> GetPerformances() => _storage.GetPerformances();

    public IEnumerable<PerformanceDBModel> GetPerformancesByCinemaHall(Guid cinemaHallId) => _storage.GetPerformancesByCinemaHall(cinemaHallId);
}
