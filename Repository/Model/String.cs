namespace Repository.Model
{
    public enum StringType
    {
        /// <summary>
        /// Unicode string using UTF-8 encoding
        /// </summary>
        Utf8,
        
        /// <summary>
        /// A platform native string. This should be utf-8 on Windows and
        /// a zero terminated guint8 array on Unix.
        /// TODO: We currently use null terminated ASCII on both platforms, which may be problematic.
        /// </summary>
        Platform
    }
    
    public class String : Symbol
    {
        public StringType StringType { get; }

        public String(string nativeName, StringType type)
            : base(new CTypeName(nativeName), new TypeName(nativeName), new SymbolName("string"))
        {
            StringType = type;
        }
    }
}
