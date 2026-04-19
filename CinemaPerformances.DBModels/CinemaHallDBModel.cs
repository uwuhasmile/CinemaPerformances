using CinemaPerformances.Common;

namespace CinemaPerformances.DBModels;

public class CinemaHallDBModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Seats { get; set; }
    public CinemaHallType Type { get; set; }

    public CinemaHallDBModel()
    {
    }

    public CinemaHallDBModel(string? name, int seats, CinemaHallType type) : this(Guid.NewGuid(), name, seats, type)
    {
    }

    public CinemaHallDBModel(Guid id, string? name, int seats, CinemaHallType type)
    {
        Id = id;
        Name = name;
        Seats = seats;
        Type = type;
    }
}