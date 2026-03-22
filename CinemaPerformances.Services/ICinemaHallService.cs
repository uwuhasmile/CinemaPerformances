using System;
using System.Collections.Generic;
using System.Text;

using CinemaPerformances.DTOModels;

namespace CinemaPerformances.Services;

public interface ICinemaHallService
{
    IEnumerable<CinemaHallListDTO> GetCinemaHalls();
    CinemaHallListDTO? GetCinemaHall(Guid id);
}
