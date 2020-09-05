using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial class VariantType : IDisposable
    {
        #region Static Member
        //public static VariantType String = new VariantType("s");
        //public static VariantType Variant = new VariantType("v");
        #endregion

        internal IntPtr Handle { get; }

        public VariantType(string type) : this(VariantType.@new(type)) {}

        internal VariantType(IntPtr handle)
        {
            Handle = handle;
        }
        
        public void Dispose()
            => VariantType.free(Handle);

        public override string ToString()
        {
            var variantType = VariantType.dup_string(Handle);
            var t = Marshal.PtrToStringAnsi(variantType);
            //TODO FREE variantType!!

            return t;
        }
    }
}