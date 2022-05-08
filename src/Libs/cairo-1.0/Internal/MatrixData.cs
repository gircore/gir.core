namespace Cairo.Internal
{
    // The gir file does not contain the fields for Cairo.Matrix
    public partial struct MatrixData
    {
        public double Xx;
        public double Yx;
        public double Xy;
        public double Yy;
        public double X0;
        public double Y0;
    }
}
