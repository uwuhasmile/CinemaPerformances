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

    public CinemaHallListDTO? GetCinemaHall(Guid id)
    {
        var cinemaHall = _cinemaHallRepository.GetCinemaHall(id);
        return cinemaHall is null ? null : new(id, cinemaHall.Name!, cinemaHall.Seats, cinemaHall.Type, _cinemaHallRepository.GetTotalDurationByCinemaHall(id));
    }

    public IEnumerable<CinemaHallListDTO> GetCinemaHalls()
    {
        foreach (var cinemaHall in _cinemaHallRepository.GetCinemaHalls())
            yield return new(cinemaHall.Id, cinemaHall.Name!, cinemaHall.Seats, cinemaHall.Type, _cinemaHallRepository.GetTotalDurationByCinemaHall(cinemaHall.Id));
    }
}
