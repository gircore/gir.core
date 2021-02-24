namespace Repository.Model
{
    public enum RecordType
    {
        /// <summary>
        /// Simple Marshal-able C-struct
        /// </summary>
        Value,
        
        /// <summary>
        /// Struct with complex behaviour (like constructors) which should be wrapped into classes.
        /// </summary>
        Ref,
        
        /// <summary>
        /// Opaque struct
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
