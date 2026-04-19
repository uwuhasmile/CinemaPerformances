using CinemaPerformances.Common;

namespace CinemaPerformances.DTOModels;

public record PerformanceEditDTO(Guid Id, Guid CinemaHallId, string Name, MovieGenre Genre, DateTime ReleaseDate, TimeSpan Start, double Duration);