namespace Pango;

public partial class Constants
{
    /// <summary>
    /// The CSS scale factor for three shrinking steps (1 / (1.2 * 1.2 * 1.2)).
    /// </summary>
    public const double XX_SMALL = 0.5787037037037;

    /// <summary>
    /// The scale factor for two shrinking steps (1 / (1.2 * 1.2)).
    /// </summary>
    public const double X_SMALL = 0.6944444444444;

    /// <summary>
    /// The scale factor for one shrinking step (1 / 1.2).
    /// </summary>
    public const double SMALL = 0.8333333333333;

    /// <summary>
    /// The scale factor for normal size (1.0).
    /// </summary>
    public const double MEDIUM = 1.0;

    /// <summary>
    /// The scale factor for one magnification step (1.2).
    /// </summary>
    public const double LARGE = 1.2;

    /// <summary>
    /// The scale factor for two magnification steps (1.2 * 1.2).
    /// </summary>
    public const double X_LARGE = 1.44;

    /// <summary>
    /// The scale factor for three magnification steps (1.2 * 1.2 * 1.2).
    /// </summary>
    public const double XX_LARGE = 1.728;
}
