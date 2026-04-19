using CinemaPerformances.Common;

namespace CinemaPerformances.DTOModels;

public record PerformanceCreateDTO(Guid CinemaHallId, string Name, MovieGenre Genre, DateTime ReleaseDate, TimeSpan Start, double Duration);