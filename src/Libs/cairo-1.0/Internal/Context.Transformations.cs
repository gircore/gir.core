using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public partial class Context
{
    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_device_to_user")]
    public static extern void DeviceToUser(ContextHandle cr, ref double x, ref double y);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_device_to_user_distance")]
    public static extern void DeviceToUserDistance(ContextHandle cr, ref double dx, ref double dy);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_matrix")]
    public static extern void GetMatrix(ContextHandle cr, MatrixHandle matrix);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_identity_matrix")]
    public static extern void IdentityMatrix(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_scale")]
    public static extern void Scale(ContextHandle cr, double sx, double sy);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_matrix")]
    public static extern void SetMatrix(ContextHandle cr, MatrixHandle matrix);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_rotate")]
    public static extern void Rotate(ContextHandle cr, double angle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_transform")]
    public static extern void Transform(ContextHandle cr, MatrixHandle matrix);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_translate")]
    public static extern void Translate(ContextHandle cr, double tx, double ty);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_user_to_device")]
    public static extern void UserToDevice(ContextHandle cr, ref double x, ref double y);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_user_to_device_distance")]
    public static extern void UserToDeviceDistance(ContextHandle cr, ref double dx, ref double dy);
}
