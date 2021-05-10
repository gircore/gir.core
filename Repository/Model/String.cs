namespace Repository.Model
{
    /// <summary>
    /// Unicode string using UTF-8 encoding
    /// </summary>
    public class Utf8String : String
    {
        public Utf8String() : base("utf8") { }
    }

    /// <summary>
    /// A platform native string. This should be utf-8 on Windows and
    /// a zero terminated guint8 array on Unix.
    /// TODO: We currently use null terminated ASCII on both platforms, which may be problematic.
    /// </summary>
    public class PlatformString : String
    {
        public PlatformString() : base("filename") { }
    }

    public abstract class String : Type
    {
        protected String(string nativeName)
            : base(new CTypeName(nativeName), new TypeName(nativeName), new SymbolName("string"))
        {
        }
    }
}
