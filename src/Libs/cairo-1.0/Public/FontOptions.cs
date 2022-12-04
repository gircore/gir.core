using System;

namespace Cairo;

public partial class FontOptions : IEquatable<FontOptions>
{
    public FontOptions()
        : this(Internal.FontOptions.Create())
    {
    }

    public FontOptions Copy()
        => new FontOptions(Internal.FontOptions.Copy(Handle));

    public override int GetHashCode()
        => (int) Internal.FontOptions.Hash(Handle);

    public override bool Equals(object? obj) => Equals(obj as FontOptions);

    public bool Equals(FontOptions? other)
        => other != null && Internal.FontOptions.Equal(Handle, other.Handle);

    public void Merge(FontOptions other)
        => Internal.FontOptions.Merge(Handle, other.Handle);

    public Status Status => Internal.FontOptions.Status(Handle);

    public Antialias Antialias
    {
        get => Internal.FontOptions.GetAntialias(Handle);
        set => Internal.FontOptions.SetAntialias(Handle, value);
    }

    public HintMetrics HintMetrics
    {
        get => Internal.FontOptions.GetHintMetrics(Handle);
        set => Internal.FontOptions.SetHintMetrics(Handle, value);
    }

    public HintStyle HintStyle
    {
        get => Internal.FontOptions.GetHintStyle(Handle);
        set => Internal.FontOptions.SetHintStyle(Handle, value);
    }

    public SubpixelOrder SubpixelOrder
    {
        get => Internal.FontOptions.GetSubpixelOrder(Handle);
        set => Internal.FontOptions.SetSubpixelOrder(Handle, value);
    }

    public string Variations
    {
        get
        {
            var result = Internal.FontOptions.GetVariations(Handle);
            return GLib.Internal.StringHelper.ToStringUtf8(result);
        }

        set => Internal.FontOptions.SetVariations(Handle, value);
    }
}
