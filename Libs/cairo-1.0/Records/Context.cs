using System;
using System.Runtime.InteropServices;
using System.Text;

namespace cairo
{
    public partial class Context
    {
        // IMPORTANT FIXME TODO: This should not be hardcoded
        private const string cairoLib = "libcairo-2";

        // IMPORTANT: We should follow cairo's guidelines on memory management
        // for language bindings. This is a quick attempt at implementing something
        // workable.

        [DllImport (cairoLib, EntryPoint = "cairo_set_source_rgba")]
        internal static extern void NativeSetSourceRgba (Native.Context.Handle cr, double red, double green, double blue, double alpha);

        public void SetSourceRgba(double red, double green, double blue, double alpha)
            => NativeSetSourceRgba(Handle, red, green, blue, alpha);

        [DllImport (cairoLib, EntryPoint = "cairo_show_text")]
        internal static extern void NativeShowText (Native.Context.Handle cr, [MarshalAs(UnmanagedType.LPUTF8Str)] string utf8);

        public void ShowText(string text)
            => NativeShowText(Handle, text);

        [DllImport (cairoLib, EntryPoint = "cairo_move_to")]
        internal static extern void NativeMoveTo (Native.Context.Handle cr, double x, double y);

        public void MoveTo(double x, double y)
            => NativeMoveTo(Handle, x, y);

        [DllImport (cairoLib, EntryPoint = "cairo_text_extents")]
        internal static extern void NativeTextExtents (Native.Context.Handle cr, [MarshalAs (UnmanagedType.LPUTF8Str)] string utf8, out TextExtents extents);

        public void TextExtents(string text, out TextExtents extents)
            => NativeTextExtents(Handle, text, out extents);

        [DllImport (cairoLib, EntryPoint = "cairo_font_extents")]
        internal static extern void NativeFontExtents (Native.Context.Handle cr, out FontExtents extents);

        public void FontExtents(out FontExtents extents)
            => NativeFontExtents(Handle, out extents);

        [DllImport (cairoLib, EntryPoint = "cairo_fill")]
        internal static extern void NativeFill (Native.Context.Handle cr);

        public void Fill()
            => NativeFill(Handle);

        [DllImport (cairoLib, EntryPoint = "cairo_rectangle")]
        internal static extern void NativeRectangle (Native.Context.Handle cr, double x, double y, double width, double height);

        public void Rectangle(double x, double y, double width, double height)
            => NativeRectangle(Handle, x, y, width, height);


        [DllImport(cairoLib, EntryPoint = "cairo_set_font_size")]
        internal static extern void NativeSetFontSize (Native.Context.Handle cr, double size);

        public void SetFontSize(double size)
            => NativeSetFontSize(Handle, size);
    }
}
