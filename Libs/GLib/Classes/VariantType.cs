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

        public VariantType(string type) : this(Native.@new(type)) { }

        internal VariantType(IntPtr handle)
        {
            Handle = handle;
        }

        #endregion

        #region Methods

        public override string? ToString()
        {
            IntPtr variantType = Native.dup_string(Handle);
            return StringHelper.ToAnsiStringAndFree(variantType);
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
            => Native.free(Handle);

        #endregion
    }
}
