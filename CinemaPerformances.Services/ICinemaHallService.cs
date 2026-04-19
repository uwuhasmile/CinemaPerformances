using CinemaPerformances.Common;
using CinemaPerformances.Common.Enums;
using CinemaPerformances.DTOModels;

namespace CinemaPerformances.Services;

public interface ICinemaHallService
{
    IAsyncEnumerable<CinemaHallListDTO> GetCinemaHalls(string? query = null, CinemaHallFilter filter = CinemaHallFilter.None, CinemaHallSorting sorting = CinemaHallSorting.None);
    Task<CinemaHallListDTO?> GetCinemaHall(Guid id);
    Task CreateCinemaHall(CinemaHallCreateDTO cinemaHall);
    Task EditCinemaHall(CinemaHallEditDTO cinemaHall);
    Task DeleteCinemaHall(Guid id);
}
