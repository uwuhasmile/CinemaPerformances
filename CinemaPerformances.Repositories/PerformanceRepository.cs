using CinemaPerformances.Common.Enums;
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
    public Task<PerformanceDBModel?> GetPerformance(Guid id) => _storage.GetPerformance(id);

    public IAsyncEnumerable<PerformanceDBModel> GetPerformances() => _storage.GetPerformances();

    public async Task<IEnumerable<PerformanceDBModel>> GetPerformancesByCinemaHall(Guid cinemaHallId, string? query, PerformanceFilter filter, PerformanceSorting sorting)
    {
        var result = await _storage.GetPerformancesByCinemaHall(cinemaHallId);

        if (!string.IsNullOrWhiteSpace(query))
            result = result.Where(x => x.Name!.StartsWith(query, StringComparison.OrdinalIgnoreCase));
        if (filter != PerformanceFilter.None)
            result = result.Where(x => (int)x.Genre + 1 == (int)filter);
        if (sorting != PerformanceSorting.None)
        {
            switch (sorting)
            {
                case PerformanceSorting.Name:
                    result = result.OrderBy(x => x.Name);
                    break;
                case PerformanceSorting.Duration:
                    result = result.OrderBy(x => x.Duration);
                    break;
                case PerformanceSorting.ReleaseDate:
                    result = result.OrderBy(x => x.ReleaseDate);
                    break;
                case PerformanceSorting.StartTime:
                    result = result.OrderBy(x => x.Start);
                    break;
            }
        }
        return result.ToList();
    }

    public Task SavePerformance(PerformanceDBModel performance) => _storage.SavePerformance(performance);

    public Task DeletePerformance(Guid id) => _storage.DeletePerformance(id);
}
