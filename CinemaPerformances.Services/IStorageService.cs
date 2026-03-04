using CinemaPerformances.DBModels;

namespace CinemaPerformances.Services;

public interface IStorageService
{
    public IEnumerable<PerformanceDBModel> GetPerformances(Guid cinemaHallId);
    public IEnumerable<CinemaHallDBModel> GetAllCinemaHalls();
}
