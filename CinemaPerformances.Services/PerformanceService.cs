using System.ComponentModel.DataAnnotations;

using CinemaPerformances.Common.Enums;
using CinemaPerformances.DBModels;
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

    public async Task<PerformanceDetailsDTO?> GetPerformance(Guid id)
    {
        var performance = await _performanceRepository.GetPerformance(id);
        return performance is null ? null : new(id, performance.Name!, performance.Genre, performance.ReleaseDate, new DateTime().Add(performance.Start), performance.Duration);
    }

    public async Task<IEnumerable<PerformanceListDTO>> GetPerformancesByCinemaHall(Guid cinemaHallId, string? query, PerformanceFilter filter, PerformanceSorting sorting)
    {
        return (await _performanceRepository.GetPerformancesByCinemaHall(cinemaHallId, query, filter, sorting))
            .Select(x => new PerformanceListDTO(x.Id, x.Name!, x.Genre, x.ReleaseDate, new DateTime().Add(x.Start), x.Duration));
    }

    public async Task CreatePerformance(PerformanceCreateDTO performance)
    {
        var errors = performance.Validate();
        if (errors.Count > 0) throw new ValidationException(string.Join(Environment.NewLine, errors.Select(x => x.Message)));
        PerformanceDBModel newPerformance = new(performance.CinemaHallId, performance.Name, performance.Genre, performance.ReleaseDate, performance.Start, performance.Duration);
        await _performanceRepository.SavePerformance(newPerformance);
    }

    public async Task EditPerformance(PerformanceEditDTO performance)
    {
        var errors = performance.Validate();
        if (errors.Count > 0) throw new ValidationException(string.Join(Environment.NewLine, errors.Select(x => x.Message)));
        PerformanceDBModel updatedPerformance = new(performance.Id, performance.CinemaHallId, performance.Name, performance.Genre, performance.ReleaseDate, performance.Start, performance.Duration);
        await _performanceRepository.SavePerformance(updatedPerformance);
    }

    public async Task DeletePerformance(Guid id)
    {
        await _performanceRepository.DeletePerformance(id);
    }
}
