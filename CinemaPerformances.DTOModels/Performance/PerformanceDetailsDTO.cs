using CinemaPerformances.Common;

namespace CinemaPerformances.DTOModels;

public record PerformanceDetailsDTO(Guid Id, string Name, MovieGenre Genre, DateTime ReleaseDate, DateTime Start, double Duration, DateTime End);