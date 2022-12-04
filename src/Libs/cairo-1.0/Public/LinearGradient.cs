namespace Cairo;

public class LinearGradient : Gradient
{
    public LinearGradient(double x0, double y0, double x1, double y1)
        : base(Internal.Pattern.CreateLinear(x0, y0, x1, y1))
    {
    }

    public Status GetLinearPoints(out double x0, out double y0, out double x1, out double y1)
        => Internal.Pattern.GetLinearPoints(Handle, out x0, out y0, out x1, out y1);
}
