using CinemaPerformances.DBModels;
using CinemaPerformances.Common;

namespace CinemaPerformances.Services;

public static class FakeStorage
{
    private static readonly List<CinemaHallDBModel> s_cinemaHalls;
    private static readonly List<PerformanceDBModel> s_performances;

    internal static IEnumerable<CinemaHallDBModel> CinemaHalls => s_cinemaHalls.ToList();

    internal static IEnumerable<PerformanceDBModel> Performances => s_performances.ToList();

    static FakeStorage()
    {
        CinemaHallDBModel redHall = new("Red Hall", 64, Common.CinemaHallType.T2D);
        CinemaHallDBModel greenHall = new("Green Hall", 64, Common.CinemaHallType.T2D);
        CinemaHallDBModel purpleHall = new("Purple Hall", 64, Common.CinemaHallType.T3D);
        CinemaHallDBModel orangeHall = new("Orange Hall", 64, Common.CinemaHallType.T3D);
        CinemaHallDBModel vipHall = new("VIP Hall", 32, Common.CinemaHallType.IMAX);
        s_cinemaHalls = [ redHall, greenHall, purpleHall, orangeHall, vipHall ];

        s_performances = [
            new(vipHall.Id, "Spider-Man: Beyond the Spider-Verse", MovieGenre.Animation, new(2025, 3, 28), new(2026, 2, 27, 14, 0, 0), 140.0),
            new(greenHall.Id, "The Grand Budapest Hotel", MovieGenre.Comedy, new(2014, 3, 28), new(2026, 2, 27, 18, 0, 0), 100.0),
            new(greenHall.Id, "Superbad", MovieGenre.Comedy, new(2007, 8, 17), new(2026, 2, 27, 21, 0, 0), 113.0),
            new(redHall.Id, "Oppenheimer", MovieGenre.Drama, new(2023, 7, 21), new(2026, 2, 27, 17, 0, 0), 180.0),
            new(vipHall.Id, "Mad Max: Fury Road", MovieGenre.Action, new(2015, 5, 15), new(2026, 2, 27, 20, 0, 0), 120.0),
            new(orangeHall.Id, "Creed III", MovieGenre.Sports, new(2023, 3, 3), new(2026, 2, 27, 19, 30, 0), 116.0),
            new(orangeHall.Id, "Parasite", MovieGenre.Thriller, new(2019, 5, 30), new(2026, 2, 27, 22, 0, 0), 132.0),
            new(purpleHall.Id, "Scream VI", MovieGenre.Horror, new(2023, 3, 10), new(2026, 2, 27, 23, 0, 0), 122.0),
            new(purpleHall.Id, "Halloween", MovieGenre.Horror, new(1978, 10, 25), new(2026, 2, 27, 19, 0, 0), 91.0)
        ];
    }
}
