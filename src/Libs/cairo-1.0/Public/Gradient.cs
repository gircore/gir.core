namespace Cairo
{
    public class Gradient : Pattern
    {
        protected Gradient(Internal.PatternHandle handle)
            : base(handle)
        {
        }

        public void AddColorStopRgb(double offset, double r, double g, double b)
            => Internal.Pattern.AddColorStopRgb(Handle, offset, r, g, b);

        public void AddColorStopRgba(double offset, double r, double g, double b, double a)
            => Internal.Pattern.AddColorStopRgba(Handle, offset, r, g, b, a);

        public int ColorStopCount
        {
            get
            {
                Internal.Pattern.GetColorStopCount(Handle, out int count);
                return count;
            }
        }

        public Status GetColorStopRgba(int index, out double offset, out double r, out double g, out double b, out double a)
            => Internal.Pattern.GetColorStopRgba(Handle, index, out offset, out r, out g, out b, out a);
    }
}
