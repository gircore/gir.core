namespace GirLoader.Output;

/// <summary>
/// Unicode string using UTF-8 encoding
/// </summary>
public class Utf8String : String, GirModel.Utf8String
{
    internal override bool Matches(TypeReference typeReference)
    {
        return typeReference.SymbolNameReference?.SymbolName == "utf8";
    }
}

/// <summary>
/// A platform native filename string. See https://docs.gtk.org/glib/character-set.html
/// </summary>
public class PlatformString : String, GirModel.PlatformString
{
    internal override bool Matches(TypeReference typeReference)
    {
        return typeReference.SymbolNameReference?.SymbolName == "filename";
    }
}

public abstract class String : Type
{
    protected String() : base("gchar*")
    {
    }
}
