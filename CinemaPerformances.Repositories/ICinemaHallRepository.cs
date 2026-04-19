using CinemaPerformances.Common;
using CinemaPerformances.Common.Enums;
using CinemaPerformances.DBModels;

namespace CinemaPerformances.Repositories;

public interface ICinemaHallRepository
{
    IAsyncEnumerable<CinemaHallDBModel> GetCinemaHalls(string? query = null, CinemaHallFilter filter = CinemaHallFilter.None, CinemaHallSorting sorting = CinemaHallSorting.None);

    Task<CinemaHallDBModel?> GetCinemaHall(Guid id);
    Task<double> GetTotalDurationByCinemaHall(Guid cinemaHallId);

    Task SaveCinemaHall(CinemaHallDBModel cinemaHall);
    Task DeleteCinemaHall(Guid id);
}
