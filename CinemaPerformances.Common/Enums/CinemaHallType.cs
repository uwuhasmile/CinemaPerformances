using System.ComponentModel.DataAnnotations;

namespace CinemaPerformances.Common;

public enum CinemaHallType
{
    [Display(Name = "2D")]
    CinemaHallType2D,
    [Display(Name = "3D")]
    CinemaHallType3D,
    [Display(Name = "IMAX")]
    CinemaHallTypeIMAX
}