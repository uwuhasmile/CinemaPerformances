using CinemaPerformances.Common;

namespace CinemaPerformances.DTOModels;

public record CinemaHallListDTO(Guid Id, string Name, int Seats, CinemaHallType Type, double TotalDuration);