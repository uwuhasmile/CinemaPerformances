using System.Collections;

using CinemaPerformances.DBModels;

namespace CinemaPerformances.Storage;

public interface IStorageContext
{
    IAsyncEnumerable<CinemaHallDBModel> GetCinemaHalls();
    Task<CinemaHallDBModel?> GetCinemaHall(Guid cinemaHallId);
    IAsyncEnumerable<PerformanceDBModel> GetPerformances();
    Task<IEnumerable<PerformanceDBModel>> GetPerformancesByCinemaHall(Guid cinemaHallId);
    Task<PerformanceDBModel?> GetPerformance(Guid id);
    Task<double> GetTotalDurationByCinemaHall(Guid cinemaHallId);
    Task SaveCinemaHall(CinemaHallDBModel cinemaHall);
    Task DeleteCinemaHall(Guid id);
    Task SavePerformance(PerformanceDBModel performance);
    Task DeletePerformance(Guid id);
}
