using CinemaPerformances.Common;

namespace CinemaPerformances.DBModels;

public class CinemaHallDBModel
{
    public readonly Guid Id;
    public string Name { get; set; }
    public int Seats { get; set; }
    public CinemaHallType Type { get; set; }
    
    private CinemaHallDBModel()
    {
    }

    public CinemaHallDBModel(string name, int seats, CinemaHallType type)
    {
        Id = Guid.NewGuid();
        Name = name;
        Seats = seats;
        Type = type;
    }
}