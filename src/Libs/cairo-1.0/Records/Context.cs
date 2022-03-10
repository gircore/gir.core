namespace cairo
{
    public partial class Context
    {
        public void Fill()
            => Internal.Context.Fill(Handle);

        public void FontExtents(out FontExtents extents)
            => Internal.Context.FontExtents(Handle, out extents);

        public void MoveTo(double x, double y)
            => Internal.Context.MoveTo(Handle, x, y);

        public void Rectangle(double x, double y, double width, double height)
            => Internal.Context.Rectangle(Handle, x, y, width, height);

        public void SetFontSize(double size)
            => Internal.Context.SetFontSize(Handle, size);

        public void SetSourceRgba(double red, double green, double blue, double alpha)
            => Internal.Context.SetSourceRgba(Handle, red, green, blue, alpha);

        public void ShowText(string text)
            => Internal.Context.ShowText(Handle, text);

        public void TextExtents(string text, out TextExtents extents)
            => Internal.Context.TextExtents(Handle, text, out extents);
    }
}
