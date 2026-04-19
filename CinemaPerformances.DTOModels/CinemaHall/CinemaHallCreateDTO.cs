using CinemaPerformances.Common;

namespace CinemaPerformances.DTOModels;

public record CinemaHallCreateDTO(string Name, int Seats, CinemaHallType Type);