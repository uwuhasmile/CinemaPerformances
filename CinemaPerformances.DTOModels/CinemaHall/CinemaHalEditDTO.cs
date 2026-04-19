using CinemaPerformances.Common;

namespace CinemaPerformances.DTOModels;

public record CinemaHallEditDTO(Guid Id, string Name, int Seats, CinemaHallType Type);