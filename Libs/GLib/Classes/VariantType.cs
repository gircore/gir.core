using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial class VariantType : IDisposable
    {
        #region Static Member

        public static readonly VariantType String = new VariantType("s");
        public static readonly VariantType Variant = new VariantType("v");

        #endregion

        #region Properties

        internal IntPtr Handle { get; }

        #endregion

        #region Constructors

        public VariantType(string type) : this(@new(type)) { }

        internal VariantType(IntPtr handle)
        {
            Handle = handle;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            IntPtr variantType = dup_string(Handle);
            var t = Marshal.PtrToStringAnsi(variantType);
            //TODO FREE variantType!!

            return t;
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
            => free(Handle);

        #endregion
    }
}
