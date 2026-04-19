using System.ComponentModel.DataAnnotations;

namespace CinemaPerformances.Common.Enums;

public enum CinemaHallFilter
{
    [Display(Name = "None")]
    None,
    [Display(Name = "2D")]
    CinemaHallFilter2D,
    [Display(Name = "3D")]
    CinemaHallFilter3D,
    [Display(Name = "IMAX")]
    CinemaHallFilterIMAX
}