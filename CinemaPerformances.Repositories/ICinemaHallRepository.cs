using CinemaPerformances.DBModels;

namespace CinemaPerformances.Repositories;

public interface ICinemaHallRepository
{
    IEnumerable<CinemaHallDBModel> GetCinemaHalls();
    CinemaHallDBModel? GetCinemaHall(Guid id);
    double GetTotalDurationByCinemaHall(Guid cinemaHallId);
}
