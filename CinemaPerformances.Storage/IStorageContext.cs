using CinemaPerformances.DBModels;

namespace CinemaPerformances.Storage;

public interface IStorageContext
{
    IEnumerable<CinemaHallDBModel> GetCinemaHalls();
    CinemaHallDBModel? GetCinemaHall(Guid cinemaHallId);
    IEnumerable<PerformanceDBModel> GetPerformances();
    IEnumerable<PerformanceDBModel> GetPerformancesByCinemaHall(Guid cinemaHallId);
    PerformanceDBModel? GetPerformance(Guid id);
    double GetTotalDurationByCinemaHall(Guid cinemaHallId);
}
