using CinemaPerformances.Common.Enums;
using CinemaPerformances.DTOModels;

namespace CinemaPerformances.Services;

public interface IPerformanceService
{
    Task<IEnumerable<PerformanceListDTO>> GetPerformancesByCinemaHall(Guid cinemaHallId, string? query = null, PerformanceFilter filter = PerformanceFilter.None, PerformanceSorting sorting = PerformanceSorting.None);
    Task<PerformanceDetailsDTO?> GetPerformance(Guid id);
    Task CreatePerformance(PerformanceCreateDTO performance);
    Task EditPerformance(PerformanceEditDTO performance);
    Task DeletePerformance(Guid id);
}
