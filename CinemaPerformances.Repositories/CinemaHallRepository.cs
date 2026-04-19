using CinemaPerformances.Common;
using CinemaPerformances.Common.Enums;
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
    public Task<CinemaHallDBModel?> GetCinemaHall(Guid id) => _storage.GetCinemaHall(id);

    public IAsyncEnumerable<CinemaHallDBModel> GetCinemaHalls(string? query, CinemaHallFilter filter, CinemaHallSorting sorting)
    {
        var result = _storage.GetCinemaHalls();

        if (!string.IsNullOrWhiteSpace(query))
            result = result.Where(x => x.Name!.StartsWith(query, StringComparison.OrdinalIgnoreCase));
        if (filter != CinemaHallFilter.None)
            result = result.Where(x => (int)x.Type + 1 == (int)filter);
        if (sorting != CinemaHallSorting.None)
        {
            switch (sorting)
            {
                case CinemaHallSorting.Name:
                    result = result.OrderBy(x => x.Name);
                    break;
                case CinemaHallSorting.Seats:
                    result = result.OrderBy(x => x.Seats);
                    break;
            }
        }
        return result;
    }
    public Task<double> GetTotalDurationByCinemaHall(Guid cinemaHallId) => _storage.GetTotalDurationByCinemaHall(cinemaHallId);

    public Task SaveCinemaHall(CinemaHallDBModel cinemaHall) => _storage.SaveCinemaHall(cinemaHall);

    public Task DeleteCinemaHall(Guid id) => _storage.DeleteCinemaHall(id);
}
