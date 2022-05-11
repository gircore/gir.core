namespace Cairo
{
    public partial class Context
    {
        // TODO
        // - Add cairo_glyph_path(), which requires cairo_glyph_t from the text API.

        public Path CopyPath()
            => new Path(Internal.Context.CopyPath(Handle));

        public Path CopyPathFlat()
            => new Path(Internal.Context.CopyPathFlat(Handle));

        public void AppendPath(Path path)
            => Internal.Context.AppendPath(Handle, path.Handle);

        public bool HasCurrentPoint
            => Internal.Context.HasCurrentPoint(Handle);

        public void GetCurrentPoint(out double x, out double y)
            => Internal.Context.GetCurrentPoint(Handle, out x, out y);

        public void NewPath()
            => Internal.Context.NewPath(Handle);

        public void NewSubPath()
            => Internal.Context.NewSubPath(Handle);

        public void ClosePath()
            => Internal.Context.ClosePath(Handle);

        public void Arc(double xc, double yc, double radius, double angle1, double angle2)
            => Internal.Context.Arc(Handle, xc, yc, radius, angle1, angle2);

        public void ArcNegative(double xc, double yc, double radius, double angle1, double angle2)
            => Internal.Context.ArcNegative(Handle, xc, yc, radius, angle1, angle2);

        public void CurveTo(double x1, double y1, double x2, double y2, double x3, double y3)
            => Internal.Context.CurveTo(Handle, x1, y1, x2, y2, x3, y3);

        public void LineTo(double x, double y)
            => Internal.Context.LineTo(Handle, x, y);

        public void MoveTo(double x, double y)
            => Internal.Context.MoveTo(Handle, x, y);

        public void Rectangle(double x, double y, double width, double height)
            => Internal.Context.Rectangle(Handle, x, y, width, height);

        public void TextPath(string text)
            => Internal.Context.TextPath(Handle, text);

        public void RelCurveTo(double x1, double y1, double x2, double y2, double x3, double y3)
            => Internal.Context.RelCurveTo(Handle, x1, y1, x2, y2, x3, y3);

        public void RelLineTo(double x, double y)
            => Internal.Context.RelLineTo(Handle, x, y);

        public void RelMoveTo(double x, double y)
            => Internal.Context.RelMoveTo(Handle, x, y);

        public void PathExtents(out double x1, out double y1, out double x2, out double y2)
            => Internal.Context.PathExtents(Handle, out x1, out y1, out x2, out y2);
    }
}
