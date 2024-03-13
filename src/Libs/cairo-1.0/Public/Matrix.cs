using Cairo.Internal;

namespace Cairo;

public partial class Matrix
{
    public Matrix() : this(MatrixOwnedHandle.Create()) { }

    public void Init(double xx, double xy, double yx, double yy, double x0, double y0)
        => Internal.Matrix.Init(Handle, xx, xy, yx, yy, x0, y0);

    public void InitIdentity()
        => Internal.Matrix.InitIdentity(Handle);

    public void InitTranslate(double tx, double ty)
        => Internal.Matrix.InitTranslate(Handle, tx, ty);

    public void InitScale(double sx, double sy)
        => Internal.Matrix.InitScale(Handle, sx, sy);

    public void InitRotate(double radians)
        => Internal.Matrix.InitRotate(Handle, radians);

    public void Translate(double tx, double ty)
        => Internal.Matrix.Translate(Handle, tx, ty);

    public void Scale(double sx, double sy)
        => Internal.Matrix.Scale(Handle, sx, sy);

    public void Rotate(double radians)
        => Internal.Matrix.Rotate(Handle, radians);

    public Status Invert()
        => Internal.Matrix.Invert(Handle);

    public void Multiply(Matrix matrix)
        => Internal.Matrix.Multiply(Handle, Handle, matrix.Handle);

    public void TransformDistance(ref double dx, ref double dy)
        => Internal.Matrix.TransformDistance(Handle, ref dx, ref dy);

    public void TransformPoint(ref double x, ref double y)
        => Internal.Matrix.TransformPoint(Handle, ref x, ref y);
}
