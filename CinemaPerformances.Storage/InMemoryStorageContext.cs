using CinemaPerformances.Common;
using CinemaPerformances.DBModels;

namespace CinemaPerformances.Storage;

public class InMemoryStorageContext : IStorageContext
{
    private record struct CinemaHallRecord(Guid Id, string Name, int Seats, CinemaHallType Type);
    private record struct PerformanceRecord(Guid Id, Guid CinemaHallId, string Name, MovieGenre Genre, DateTime ReleaseDate, TimeSpan Start, double Duration);

    private static readonly List<CinemaHallRecord> _cinemaHalls;
    private static readonly List<PerformanceRecord> _performances;

    static InMemoryStorageContext()
    {
        CinemaHallRecord redHall = new(Guid.NewGuid(), "Red Hall", 64, Common.CinemaHallType.CinemaHallType2D);
        CinemaHallRecord greenHall = new(Guid.NewGuid(), "Green Hall", 64, Common.CinemaHallType.CinemaHallType2D);
        CinemaHallRecord purpleHall = new(Guid.NewGuid(), "Purple Hall", 64, Common.CinemaHallType.CinemaHallType3D);
        CinemaHallRecord orangeHall = new(Guid.NewGuid(), "Orange Hall", 64, Common.CinemaHallType.CinemaHallType3D);
        CinemaHallRecord vipHall = new(Guid.NewGuid(), "VIP Hall", 32, Common.CinemaHallType.CinemaHallTypeIMAX);
        _cinemaHalls = [redHall, greenHall, purpleHall, orangeHall, vipHall];

        _performances = [
            new(Guid.NewGuid(), vipHall.Id, "Spider-Man: Beyond the Spider-Verse", MovieGenre.Animation, new(2025, 3, 28), new(14, 0, 0), 140.0),
            new(Guid.NewGuid(), greenHall.Id, "The Grand Budapest Hotel", MovieGenre.Comedy, new(2014, 3, 28), new(18, 0, 0), 100.0),
            new(Guid.NewGuid(),greenHall.Id, "Superbad", MovieGenre.Comedy, new(2007, 8, 17), new(21, 0, 0), 113.0),
            new(Guid.NewGuid(),redHall.Id, "Oppenheimer", MovieGenre.Drama, new(2023, 7, 21), new(17, 0, 0), 180.0),
            new(Guid.NewGuid(),vipHall.Id, "Mad Max: Fury Road", MovieGenre.Action, new(2015, 5, 15), new(20, 0, 0), 120.0),
            new(Guid.NewGuid(),orangeHall.Id, "Creed III", MovieGenre.Sports, new(2023, 3, 3), new(19, 30, 0), 116.0),
            new(Guid.NewGuid(),orangeHall.Id, "Parasite", MovieGenre.Thriller, new(2019, 5, 30), new(22, 0, 0), 132.0),
            new(Guid.NewGuid(),purpleHall.Id, "Scream VI", MovieGenre.Horror, new(2023, 3, 10), new(23, 0, 0), 122.0),
            new(Guid.NewGuid(),purpleHall.Id, "Halloween", MovieGenre.Horror, new(1978, 10, 25), new(19, 0, 0), 91.0)
        ];
    }

    public async Task<CinemaHallDBModel?> GetCinemaHall(Guid cinemaHallId)
    {
        var cinemaHall = _cinemaHalls.FirstOrDefault(cinemaHall => cinemaHall.Id.Equals(cinemaHallId));
        return cinemaHall.Id.Equals(Guid.Empty) ? null : new(cinemaHall.Id, cinemaHall.Name, cinemaHall.Seats, cinemaHall.Type);
    }

    public async IAsyncEnumerable<CinemaHallDBModel> GetCinemaHalls()
    {
        foreach (var cinemaHall in _cinemaHalls)
        {
            await Task.Delay(10);
            yield return new(cinemaHall.Id, cinemaHall.Name, cinemaHall.Seats, cinemaHall.Type);
        }
    }

    public async Task<double> GetTotalDurationByCinemaHall(Guid cinemaHallId)
    {
        await Task.Delay(10);
        double result = 0.0;
        foreach (var performance in _performances)
            if (performance.CinemaHallId.Equals(cinemaHallId))
                result += performance.Duration;
        return result;
    }

    public Task<IEnumerable<PerformanceDBModel>> GetPerformancesByCinemaHall(Guid cinemaHallId)
    {
        return Task.Run(() =>
        {
            return _performances.Where(x => x.CinemaHallId == cinemaHallId).Select(x => new PerformanceDBModel(x.Id, x.CinemaHallId, x.Name, x.Genre, x.ReleaseDate, x.Start, x.Duration));
        });
    }

    public async IAsyncEnumerable<PerformanceDBModel> GetPerformances()
    {
        foreach (var performance in _performances)
        {
            await Task.Delay(10);
            yield return new(performance.Id, performance.CinemaHallId, performance.Name, performance.Genre, performance.ReleaseDate, performance.Start, performance.Duration);
        }
    }

    public async Task<PerformanceDBModel?> GetPerformance(Guid id)
    {
        await Task.Delay(10);
        var performance = _performances.FirstOrDefault(performance => performance.Id.Equals(id));
        return performance.Id.Equals(Guid.Empty) ? null : new(performance.Id, performance.CinemaHallId, performance.Name, performance.Genre, performance.ReleaseDate, performance.Start, performance.Duration);
    }

    public Task SaveCinemaHall(CinemaHallDBModel cinemaHall)
    {
        throw new NotSupportedException();
    }

    public Task DeleteCinemaHall(Guid id)
    {
        throw new NotSupportedException();
    }

    public Task SavePerformance(PerformanceDBModel performance)
    {
        throw new NotImplementedException();
    }

    public Task DeletePerformance(Guid id)
    {
        throw new NotSupportedException();
    }
}
