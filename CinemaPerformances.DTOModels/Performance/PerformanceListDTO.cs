using CinemaPerformances.Common;

namespace CinemaPerformances.DTOModels;

public record PerformanceListDTO(Guid Id, string Name, MovieGenre Genre, DateTime ReleaseDate, DateTime Start, double Duration);
