namespace Pango;

public partial class Functions
{
    /*
     * The following functions are derived from function macros:
     * https://gitlab.gnome.org/GNOME/pango/-/blob/main/pango/pango-types.h
     */

    /// <summary>
    /// Converts a dimension to Pango units by multiplying by the Pango scale.
    /// </summary>
    /// <param name="pixels">A dimension in device units</param>
    /// <returns>Dimension in Pango units</returns>
    public static int FromPixels(int pixels)
    {
        return pixels * Constants.SCALE;
    }

    /// <summary>
    /// Converts a dimension to device units by rounding.
    /// </summary>
    /// <param name="units">A dimension in Pango units</param>
    /// <returns>Rounded dimension in device units</returns>
    public static int ToPixels(int units)
    {
        return (units + Constants.SCALE / 2) >> 10;
    }

    /// <summary>
    /// Converts a dimension to device units by flooring.
    /// </summary>
    /// <param name="units">A dimension in Pango units</param>
    /// <returns>Floored dimension in device units</returns>
    public static int ToPixelsFloor(int units)
    {
        return units >> 10;
    }

    /// <summary>
    /// Converts a dimension to device units by ceiling.
    /// </summary>
    /// <param name="units">A dimension in Pango units</param>
    /// <returns>Ceiled dimension in device units</returns>
    public static int ToPixelsCeil(int units)
    {
        return units & ~(Constants.SCALE - 1);
    }

    /// <summary>
    /// Rounds a dimension down to whole device units, but does not convert it to device units.
    /// </summary>
    /// <param name="units">A dimension in Pango units</param>
    /// <returns>Rounded down dimension in device units</returns>
    public static int UnitsFloor(int units)
    {
        return (units + Constants.SCALE - 1) >> 10;
    }

    /// <summary>
    /// Rounds a dimension up to whole device units, but does not convert it to device units.
    /// </summary>
    /// <param name="units">A dimension in Pango units</param>
    /// <returns>Rounded up dimension in device units</returns>
    public static int UnitsCeil(int units)
    {
        return (units + (Constants.SCALE - 1)) & ~(Constants.SCALE - 1);
    }

    /// <summary>
    /// Rounds a dimension to whole device units, but does not convert it to device units.
    /// </summary>
    /// <param name="units">A dimension in Pango units</param>
    /// <returns>Rounded dimension in device units</returns>
    public static int UnitsRound(int units)
    {
        return (units + (Constants.SCALE >> 1)) & ~(Constants.SCALE - 1);
    }
}
