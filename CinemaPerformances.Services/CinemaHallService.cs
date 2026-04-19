using System.ComponentModel.DataAnnotations;

using CinemaPerformances.Common;
using CinemaPerformances.Common.Enums;
using CinemaPerformances.DBModels;
using CinemaPerformances.DTOModels;
using CinemaPerformances.Repositories;

namespace CinemaPerformances.Services;

public class CinemaHallService : ICinemaHallService
{
    private readonly ICinemaHallRepository _cinemaHallRepository;

    public CinemaHallService(ICinemaHallRepository cinemaHallRepository)
    {
        _cinemaHallRepository = cinemaHallRepository;
    }

    public async Task<CinemaHallListDTO?> GetCinemaHall(Guid id)
    {
        var cinemaHall = await _cinemaHallRepository.GetCinemaHall(id);
        return cinemaHall is null ? null : new(id, cinemaHall.Name!, cinemaHall.Seats, cinemaHall.Type, await _cinemaHallRepository.GetTotalDurationByCinemaHall(id));
    }

    public async IAsyncEnumerable<CinemaHallListDTO> GetCinemaHalls(string? query, CinemaHallFilter filter, CinemaHallSorting sorting)
    {
        await foreach (var cinemaHall in _cinemaHallRepository.GetCinemaHalls(query, filter, sorting))
        {
            yield return new(cinemaHall.Id, cinemaHall.Name!, cinemaHall.Seats, cinemaHall.Type, await _cinemaHallRepository.GetTotalDurationByCinemaHall(cinemaHall.Id));
        }
    }

    public async Task CreateCinemaHall(CinemaHallCreateDTO cinemaHall)
    {
        var errors = cinemaHall.Validate();
        if (errors.Count > 0) throw new ValidationException(string.Join(Environment.NewLine, errors.Select(x => x.Message)));
        CinemaHallDBModel newCinemaHall = new(cinemaHall.Name, cinemaHall.Seats, cinemaHall.Type);
        await _cinemaHallRepository.SaveCinemaHall(newCinemaHall);
    }

    public async Task EditCinemaHall(CinemaHallEditDTO cinemaHall)
    {
        var errors = cinemaHall.Validate();
        if (errors.Count > 0) throw new ValidationException(string.Join(Environment.NewLine, errors.Select(x => x.Message)));
        CinemaHallDBModel updatedCinemaHall = new(cinemaHall.Id, cinemaHall.Name, cinemaHall.Seats, cinemaHall.Type);
        await _cinemaHallRepository.SaveCinemaHall(updatedCinemaHall);
    }

    public async Task DeleteCinemaHall(Guid id)
    {
        await _cinemaHallRepository.DeleteCinemaHall(id);
    }
}
