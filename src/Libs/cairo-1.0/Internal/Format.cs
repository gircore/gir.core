using System.Runtime.InteropServices;

namespace Cairo.Internal;

public class Format
{
    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_format_stride_for_width")]
    public static extern int StrideForWidth(Cairo.Format format, int width);
}
