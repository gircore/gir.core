using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal
{
    public class Matrix
    {
        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_matrix_init")]
        public static extern void Init(MatrixHandle matrix, double xx, double xy, double yx, double yy, double x0, double y0);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_matrix_init_identity")]
        public static extern void InitIdentity(MatrixHandle matrix);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_matrix_init_rotate")]
        public static extern void InitRotate(MatrixHandle matrix, double radians);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_matrix_init_scale")]
        public static extern void InitScale(MatrixHandle matrix, double sx, double sy);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_matrix_init_translate")]
        public static extern void InitTranslate(MatrixHandle matrix, double tx, double ty);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_matrix_invert")]
        public static extern Status Invert(MatrixHandle matrix);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_matrix_multiply")]
        public static extern void Multiply(MatrixHandle result, MatrixHandle a, MatrixHandle b);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_matrix_rotate")]
        public static extern void Rotate(MatrixHandle matrix, double radians);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_matrix_scale")]
        public static extern void Scale(MatrixHandle matrix, double sx, double sy);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_matrix_transform_distance")]
        public static extern void TransformDistance(MatrixHandle matrix, ref double dx, ref double dy);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_matrix_transform_point")]
        public static extern void TransformPoint(MatrixHandle matrix, ref double x, ref double y);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_matrix_translate")]
        public static extern void Translate(MatrixHandle matrix, double tx, double ty);
    }
}
