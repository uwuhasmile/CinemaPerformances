using CinemaPerformances.Common.Enums;
using CinemaPerformances.DBModels;

namespace CinemaPerformances.Repositories;

public interface IPerformanceRepository
{
    Task<IEnumerable<PerformanceDBModel>> GetPerformancesByCinemaHall(Guid cinemaHallId, string? query = null, PerformanceFilter filter = PerformanceFilter.None, PerformanceSorting sorting = PerformanceSorting.None);

    Task<PerformanceDBModel?> GetPerformance(Guid id);
    IAsyncEnumerable<PerformanceDBModel> GetPerformances();

    Task SavePerformance(PerformanceDBModel performance);
    Task DeletePerformance(Guid id);
}
