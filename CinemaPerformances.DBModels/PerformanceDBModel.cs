using CinemaPerformances.Common;

namespace CinemaPerformances.DBModels;

public class PerformanceDBModel
{
    public Guid Id { get; set; }
    public Guid CinemaHallId { get; set; }
    public string? Name { get; set; }
    public MovieGenre Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
    public TimeSpan Start { get; set; }
    public double Duration { get; set; }

    public PerformanceDBModel()
    {
    }

    public PerformanceDBModel(Guid cinemaHallId, string? name, MovieGenre genre, DateTime releaseDate, TimeSpan start, double duration)
        : this(Guid.NewGuid(), cinemaHallId, name, genre, releaseDate, start, duration)
    {
    }

    public PerformanceDBModel(Guid id, Guid cinemaHallId, string? name, MovieGenre genre, DateTime releaseDate, TimeSpan start, double duration)
    {
        Id = id;
        CinemaHallId = cinemaHallId;
        Name = name;
        Genre = genre;
        ReleaseDate = releaseDate;
        Start = start;
        Duration = duration;
    }
}