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

        public void ShowText(string text)
            => Internal.Context.ShowText(Handle, text);

        public void TextExtents(string text, out TextExtents extents)
            => Internal.Context.TextExtents(Handle, text, out extents);
    }
}
