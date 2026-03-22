using CinemaPerformances.DBModels;
using CinemaPerformances.Storage;

namespace CinemaPerformances.Repositories;

public class CinemaHallRepository : ICinemaHallRepository
{
    private readonly IStorageContext _storage;

    public CinemaHallRepository(IStorageContext storage)
    {
        _storage = storage;
    }

    public CinemaHallDBModel? GetCinemaHall(Guid id) => _storage.GetCinemaHall(id);

    public IEnumerable<CinemaHallDBModel> GetCinemaHalls() => _storage.GetCinemaHalls();

    public double GetTotalDurationByCinemaHall(Guid cinemaHallId) => _storage.GetTotalDurationByCinemaHall(cinemaHallId);
}
