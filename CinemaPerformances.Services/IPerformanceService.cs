using CinemaPerformances.DTOModels;

namespace CinemaPerformances.Services;

public interface IPerformanceService
{
    IEnumerable<PerformanceListDTO> GetPerformancesByCinemaHall(Guid cinemaHallId);
    PerformanceDetailsDTO? GetPerformance(Guid id);
}
