using CinemaPerformances.Common;

namespace CinemaPerformances.DBModels;

public class PerformanceDBModel
{
    public readonly Guid Id;
    public readonly Guid CinemaHallId;
    public string Name { get; set; }
    public MovieGenre Genre { get; set; }
    public readonly DateTime ReleaseDate;
    public DateTime Start { get; set; }
    public double Duration { get; set; }

    private PerformanceDBModel()
    {
    }

    public PerformanceDBModel(Guid cinemaHallId, string name, MovieGenre genre, DateTime releaseDate, DateTime start, double duration)
    {
        Id = Guid.NewGuid();
        CinemaHallId = cinemaHallId;
        Name = name;
        Genre = genre;
        ReleaseDate = releaseDate;
        Start = start;
        Duration = duration;
    }
}