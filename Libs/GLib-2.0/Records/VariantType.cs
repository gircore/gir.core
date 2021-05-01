using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial record VariantType : IDisposable
    {
        #region Static Member

        public static readonly VariantType String = new VariantType("s");
        public static readonly VariantType Variant = new VariantType("v");

        #endregion

        #region Properties

        internal Native.VariantType.Handle Handle { get; }

        #endregion

        #region Constructors

        public VariantType(string type) : this(Native.VariantType.Methods.New(type)) { }

        internal VariantType(Native.VariantType.Handle handle)
        {
            Handle = handle;
        }

        #endregion

        #region Methods

        public override string? ToString()
            => Marshal.PtrToStringAnsi(Native.VariantType.Methods.PeekString(Handle));

        #endregion

        #region IDisposable Implementation

        public void Dispose()
            => Handle.Dispose();

        #endregion
    }
}
