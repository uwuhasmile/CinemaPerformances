using CinemaPerformances.DBModels;

namespace CinemaPerformances.Repositories;

public interface IPerformanceRepository
{
    IEnumerable<PerformanceDBModel> GetPerformancesByCinemaHall(Guid cinemaHallId);

    PerformanceDBModel? GetPerformance(Guid id);
    IEnumerable<PerformanceDBModel> GetPerformances();
}
