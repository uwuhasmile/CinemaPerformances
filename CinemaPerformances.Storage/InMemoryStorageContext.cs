using CinemaPerformances.Common;
using CinemaPerformances.DBModels;

namespace CinemaPerformances.Storage;

public class InMemoryStorageContext : IStorageContext
{
    private record struct CinemaHallRecord(Guid Id, string Name, int Seats, CinemaHallType Type);
    private record struct PerformanceRecord(Guid Id, Guid CinemaHallId, string Name, MovieGenre Genre, DateTime ReleaseDate, DateTime Start, double Duration);

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
            new(Guid.NewGuid(), vipHall.Id, "Spider-Man: Beyond the Spider-Verse", MovieGenre.Animation, new(2025, 3, 28), new(2026, 2, 27, 14, 0, 0), 140.0),
            new(Guid.NewGuid(), greenHall.Id, "The Grand Budapest Hotel", MovieGenre.Comedy, new(2014, 3, 28), new(2026, 2, 27, 18, 0, 0), 100.0),
            new(Guid.NewGuid(),greenHall.Id, "Superbad", MovieGenre.Comedy, new(2007, 8, 17), new(2026, 2, 27, 21, 0, 0), 113.0),
            new(Guid.NewGuid(),redHall.Id, "Oppenheimer", MovieGenre.Drama, new(2023, 7, 21), new(2026, 2, 27, 17, 0, 0), 180.0),
            new(Guid.NewGuid(),vipHall.Id, "Mad Max: Fury Road", MovieGenre.Action, new(2015, 5, 15), new(2026, 2, 27, 20, 0, 0), 120.0),
            new(Guid.NewGuid(),orangeHall.Id, "Creed III", MovieGenre.Sports, new(2023, 3, 3), new(2026, 2, 27, 19, 30, 0), 116.0),
            new(Guid.NewGuid(),orangeHall.Id, "Parasite", MovieGenre.Thriller, new(2019, 5, 30), new(2026, 2, 27, 22, 0, 0), 132.0),
            new(Guid.NewGuid(),purpleHall.Id, "Scream VI", MovieGenre.Horror, new(2023, 3, 10), new(2026, 2, 27, 23, 0, 0), 122.0),
            new(Guid.NewGuid(),purpleHall.Id, "Halloween", MovieGenre.Horror, new(1978, 10, 25), new(2026, 2, 27, 19, 0, 0), 91.0)
        ];
    }

    public CinemaHallDBModel? GetCinemaHall(Guid cinemaHallId)
    {
        var cinemaHall = _cinemaHalls.FirstOrDefault(cinemaHall => cinemaHall.Id.Equals(cinemaHallId));
        return cinemaHall.Id.Equals(Guid.Empty) ? null : new(cinemaHall.Id, cinemaHall.Name, cinemaHall.Seats, cinemaHall.Type);
    }

    public IEnumerable<CinemaHallDBModel> GetCinemaHalls()
    {
        foreach (var cinemaHall in _cinemaHalls)
            yield return new(cinemaHall.Id, cinemaHall.Name, cinemaHall.Seats, cinemaHall.Type);
    }

    public double GetTotalDurationByCinemaHall(Guid cinemaHallId)
    {
        double result = 0.0;
        foreach (var performance in _performances)
            if (performance.CinemaHallId.Equals(cinemaHallId))
                result += performance.Duration;
        return result;
    }

    public IEnumerable<PerformanceDBModel> GetPerformancesByCinemaHall(Guid cinemaHallId)
    {
        foreach (var performance in _performances)
            if (performance.CinemaHallId.Equals(cinemaHallId))
                yield return new(performance.Id, performance.CinemaHallId, performance.Name, performance.Genre, performance.ReleaseDate, performance.Start, performance.Duration);
    }

    public IEnumerable<PerformanceDBModel> GetPerformances()
    {
        foreach (var performance in _performances)
            yield return new(performance.Id, performance.CinemaHallId, performance.Name, performance.Genre, performance.ReleaseDate, performance.Start, performance.Duration);
    }

    public PerformanceDBModel? GetPerformance(Guid id)
    {
        var performance = _performances.FirstOrDefault(performance => performance.Id.Equals(id));
        return performance.Id.Equals(Guid.Empty) ? null : new(performance.Id, performance.CinemaHallId, performance.Name, performance.Genre, performance.ReleaseDate, performance.Start, performance.Duration);
    }
}
