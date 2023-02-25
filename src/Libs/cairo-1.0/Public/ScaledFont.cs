using System;

namespace Cairo;

public partial class ScaledFont
{
    // TODO add methods using cairo_glyph_t
    // - cairo_scaled_font_glyph_extents()
    // - cairo_scaled_font_text_to_glyphs()

    public ScaledFont(FontFace font_face, Matrix font_matrix, Matrix ctm, FontOptions options)
        : this(Internal.ScaledFont.Create(font_face.Handle, font_matrix.Handle, ctm.Handle, options.Handle))
    {
    }

    public Status Status => Internal.ScaledFont.Status(Handle);

    public FontType FontType => Internal.ScaledFont.GetType(Handle);

    public void Extents(out FontExtents extents)
        => Internal.ScaledFont.Extents(Handle, out extents);

    public void TextExtents(string text, out TextExtents extents)
        => Internal.ScaledFont.TextExtents(Handle, GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(text), out extents);

    public FontFace GetFontFace()
        => new FontFace(Internal.ScaledFont.GetFontFace(Handle));

    public void GetFontOptions(FontOptions options)
        => Internal.ScaledFont.GetFontOptions(Handle, options.Handle);

    public void GetFontMatrix(Matrix matrix)
        => Internal.ScaledFont.GetFontMatrix(Handle, matrix.Handle);

    public void GetCtm(Matrix ctm)
        => Internal.ScaledFont.GetCtm(Handle, ctm.Handle);

    public void GetScaleMatrix(Matrix scale_matrix)
        => Internal.ScaledFont.GetScaleMatrix(Handle, scale_matrix.Handle);
}
