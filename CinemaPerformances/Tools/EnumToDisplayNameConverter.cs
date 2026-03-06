using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using CinemaPerformances.Common;

namespace CinemaPerformances.Tools;

public class EnumToDisplayNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return string.Empty;

        if (value is not Enum enumeration)
            return value.ToString() ?? string.Empty;

        return enumeration.GetDisplayName();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
