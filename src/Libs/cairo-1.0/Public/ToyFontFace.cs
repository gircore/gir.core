using System;

namespace Cairo;

public class ToyFontFace : FontFace
{
    public ToyFontFace(string family, FontSlant slant, FontWeight weight)
        : base(Internal.ToyFontFace.Create(family, slant, weight))
    {
    }

    public string Family
        => GLib.Internal.StringHelper.ToStringUtf8(Internal.ToyFontFace.GetFamily(Handle));

    public FontSlant Slant => Internal.ToyFontFace.GetSlant(Handle);

    public FontWeight Weight => Internal.ToyFontFace.GetWeight(Handle);
}
