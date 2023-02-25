using System;

namespace Cairo;

public class ToyFontFace : FontFace
{
    public ToyFontFace(string family, FontSlant slant, FontWeight weight)
        : base(Internal.ToyFontFace.Create(GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(family), slant, weight))
    {
    }

    public string Family => Internal.ToyFontFace.GetFamily(Handle).ConvertToString();

    public FontSlant Slant => Internal.ToyFontFace.GetSlant(Handle);

    public FontWeight Weight => Internal.ToyFontFace.GetWeight(Handle);
}
