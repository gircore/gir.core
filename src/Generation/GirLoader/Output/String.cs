namespace GirLoader.Output
{
    /// <summary>
    /// Unicode string using UTF-8 encoding
    /// </summary>
    public class Utf8String : String, GirModel.Utf8String
    {
        internal override bool Matches(TypeReference typeReference)
        {
            return typeReference.SymbolNameReference?.SymbolName.Value == "utf8";
        }
    }

    /// <summary>
    /// A platform native string. This should be utf-8 on Windows and
    /// a zero terminated guint8 array on Unix.
    /// TODO: We currently use null terminated ASCII on both platforms, which may be problematic.
    /// </summary>
    public class PlatformString : String, GirModel.PlatformString
    {
        internal override bool Matches(TypeReference typeReference)
        {
            return typeReference.SymbolNameReference?.SymbolName.Value == "filename";
        }
    }

    public abstract class String : PrimitiveType
    {
        protected String() : base(new CType("gchar*"), new TypeName("string"))
        {
        }
    }
}
