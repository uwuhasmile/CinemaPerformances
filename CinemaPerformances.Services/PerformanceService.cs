using CinemaPerformances.DTOModels;
using CinemaPerformances.Repositories;

namespace CinemaPerformances.Services;

public class PerformanceService : IPerformanceService
{
    private readonly IPerformanceRepository _performanceRepository;

    public PerformanceService(IPerformanceRepository performanceRepository)
    {
        _performanceRepository = performanceRepository;
    }

    public PerformanceDetailsDTO? GetPerformance(Guid id)
    {
        var performance = _performanceRepository.GetPerformance(id);
        return performance is null ? null : new(id, performance.Name!, performance.Genre, performance.ReleaseDate, performance.Start, performance.Duration, performance.Start.AddMinutes(performance.Duration));
    }

    public IEnumerable<PerformanceListDTO> GetPerformancesByCinemaHall(Guid cinemaHallId)
    {
        foreach (var department in _performanceRepository.GetPerformancesByCinemaHall(cinemaHallId))
            yield return new(department.Id, department.Name!, department.Genre, department.ReleaseDate, department.Start, department.Duration);
    }
}
