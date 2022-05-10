namespace Cairo
{
    public partial class Context
    {
        public Context(Surface target)
            : this(Internal.Context.Create(target.Handle))
        {
        }

        public Status Status => Internal.Context.Status(Handle);

        public void Save() => Internal.Context.Save(Handle);

        public void Restore() => Internal.Context.Restore(Handle);

        public Surface GetTarget()
            => new Surface(Internal.Context.GetTarget(Handle));

        #region Groups
        public void PushGroup()
            => Internal.Context.PushGroup(Handle);

        public void PushGroupWithContent(Content content)
            => Internal.Context.PushGroupWithContent(Handle, content);

        public Pattern PopGroup()
            => new Pattern(Internal.Context.PopGroup(Handle));

        public void PopGroupToSource()
            => Internal.Context.PopGroupToSource(Handle);

        public Surface GetGroupTarget()
            => new Surface(Internal.Context.GetGroupTarget(Handle));
        #endregion

        #region Source Pattern
        public void SetSourceRgb(double red, double green, double blue)
            => Internal.Context.SetSourceRgb(Handle, red, green, blue);

        public void SetSourceRgba(double red, double green, double blue, double alpha)
            => Internal.Context.SetSourceRgba(Handle, red, green, blue, alpha);

        public void SetSource(Pattern source)
            => Internal.Context.SetSource(Handle, source.Handle);

        public void SetSourceSurface(Surface surface, double x, double y)
            => Internal.Context.SetSourceSurface(Handle, surface.Handle, x, y);

        public Pattern GetSource()
            => new Pattern(Internal.Context.GetSource(Handle));
        #endregion

        #region Properties
        public Antialias Antialias
        {
            get => Internal.Context.GetAntialias(Handle);
            set => Internal.Context.SetAntialias(Handle, value);
        }

        public FillRule FillRule
        {
            get => Internal.Context.GetFillRule(Handle);
            set => Internal.Context.SetFillRule(Handle, value);
        }

        public LineCap LineCap
        {
            get => Internal.Context.GetLineCap(Handle);
            set => Internal.Context.SetLineCap(Handle, value);
        }

        public LineJoin LineJoin
        {
            get => Internal.Context.GetLineJoin(Handle);
            set => Internal.Context.SetLineJoin(Handle, value);
        }

        public double LineWidth
        {
            get => Internal.Context.GetLineWidth(Handle);
            set => Internal.Context.SetLineWidth(Handle, value);
        }

        public double MiterLimit
        {
            get => Internal.Context.GetMiterLimit(Handle);
            set => Internal.Context.SetMiterLimit(Handle, value);
        }

        public Operator Operator
        {
            get => Internal.Context.GetOperator(Handle);
            set => Internal.Context.SetOperator(Handle, value);
        }

        public double Tolerance
        {
            get => Internal.Context.GetTolerance(Handle);
            set => Internal.Context.SetTolerance(Handle, value);
        }
        #endregion

        #region Dash
        public void SetDash(double[] dashes, double offset)
            => Internal.Context.SetDash(Handle, dashes, dashes.Length, offset);

        public int DashCount => Internal.Context.GetDashCount(Handle);

        public void GetDash(out double[] dashes, out double offset)
        {
            dashes = new double[DashCount];
            Internal.Context.GetDash(Handle, dashes, out offset);
        }
        #endregion

        #region Clip
        public void Clip()
            => Internal.Context.Clip(Handle);

        public void ClipPreserve()
            => Internal.Context.ClipPreserve(Handle);

        public void ClipExtents(out double x1, out double y1, out double x2, out double y2)
            => Internal.Context.ClipExtents(Handle, out x1, out y1, out x2, out y2);

        public bool InClip(double x, double y)
            => Internal.Context.InClip(Handle, x, y);

        public void ResetClip()
            => Internal.Context.ResetClip(Handle);
        #endregion

        #region Drawing
        public void Fill()
            => Internal.Context.Fill(Handle);

        public void FillPreserve()
            => Internal.Context.FillPreserve(Handle);

        public void FillExtents(out double x1, out double y1, out double x2, out double y2)
            => Internal.Context.FillExtents(Handle, out x1, out y1, out x2, out y2);

        public bool InFill(double x, double y)
            => Internal.Context.InFill(Handle, x, y);

        public void Mask(Pattern source)
            => Internal.Context.Mask(Handle, source.Handle);

        public void MaskSurface(Surface surface, double surface_x, double surface_y)
            => Internal.Context.MaskSurface(Handle, surface.Handle, surface_x, surface_y);

        public void Paint()
            => Internal.Context.Paint(Handle);

        public void PaintWithAlpha(double alpha)
            => Internal.Context.PaintWithAlpha(Handle, alpha);

        public void Stroke()
            => Internal.Context.Stroke(Handle);

        public void StrokePreserve()
            => Internal.Context.StrokePreserve(Handle);

        public void StrokeExtents(out double x1, out double y1, out double x2, out double y2)
            => Internal.Context.StrokeExtents(Handle, out x1, out y1, out x2, out y2);

        public bool InStroke(double x, double y)
            => Internal.Context.InStroke(Handle, x, y);
        #endregion

        #region Pages
        public void CopyPage()
            => Internal.Context.CopyPage(Handle);

        public void ShowPage()
            => Internal.Context.ShowPage(Handle);
        #endregion

        public void FontExtents(out FontExtents extents)
            => Internal.Context.FontExtents(Handle, out extents);

        public void MoveTo(double x, double y)
            => Internal.Context.MoveTo(Handle, x, y);

        public void Rectangle(double x, double y, double width, double height)
            => Internal.Context.Rectangle(Handle, x, y, width, height);

        public void SetFontSize(double size)
            => Internal.Context.SetFontSize(Handle, size);

        public void ShowText(string text)
            => Internal.Context.ShowText(Handle, text);

        public void TextExtents(string text, out TextExtents extents)
            => Internal.Context.TextExtents(Handle, text, out extents);
    }
}
