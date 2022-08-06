namespace Cairo
{
    public class RadialGradient : Gradient
    {
        public RadialGradient(double cx0, double cy0, double radius0, double cx1, double cy1, double radius1)
            : base(Internal.Pattern.CreateRadial(cx0, cy0, radius0, cx1, cy1, radius1))
        {
        }
    }
}
