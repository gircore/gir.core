namespace Repository.Model
{
    public enum RecordType
    {
        /// <summary>
        /// Simple Marshal-able C-struct
        /// </summary>
        Value,
        
        /// <summary>
        /// Opaque struct, marshal as class + IntPtr
        /// </summary>
        Opaque,
        
        /// <summary>
        /// GObject type struct (special case)
        /// </summary>
        PublicClass,
        
        /// <summary>
        /// Same as PublicClass, but opaque
        /// </summary>
        PrivateClass
    }
}
