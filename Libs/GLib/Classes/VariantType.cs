using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial class VariantType : IDisposable
    {
        #region Static fields
        public static readonly VariantType String = new VariantType("s");
        public static readonly VariantType Variant = new VariantType("v");
        #endregion

        #region Fields
        internal IntPtr Handle { get; }
        #endregion
        
        #region Constructors
        public VariantType(string type) : this(VariantType.@new(type)) {}

        internal VariantType(IntPtr handle)
        {
            Handle = handle;
        }
        #endregion
        
        #region Methods
        public void Dispose()
            => VariantType.free(Handle);

        public override string ToString()
        {
            var variantType = VariantType.dup_string(Handle);
            var t = Marshal.PtrToStringAnsi(variantType);
            //TODO FREE variantType!!

            return t;
        }
        #endregion
    }
}
