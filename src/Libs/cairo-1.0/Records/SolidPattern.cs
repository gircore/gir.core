namespace Cairo
{
    public class SolidPattern : Pattern
    {
        private SolidPattern(Internal.PatternHandle handle)
            : base(handle)
        {
        }

        public static SolidPattern CreateRgb(double r, double g, double b)
            => new SolidPattern(Internal.Pattern.CreateRgb(r, g, b));

        public static SolidPattern CreateRgba(double r, double g, double b, double a)
            => new SolidPattern(Internal.Pattern.CreateRgba(r, g, b, a));

        public Status GetRgba(out double r, out double g, out double b, out double a)
            => Internal.Pattern.GetRgba(Handle, out r, out g, out b, out a);
    }
}
