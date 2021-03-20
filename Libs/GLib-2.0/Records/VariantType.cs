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

        internal Native.VariantTypeSafeHandle Handle { get; }

        #endregion

        #region Constructors

        public VariantType(string type) : this(Native.Methods.New(type)) { }

        internal VariantType(Native.VariantTypeSafeHandle handle)
        {
            Handle = handle;
        }

        #endregion

        #region Methods

        public override string? ToString()
            => Native.Methods.DupString(Handle);

        #endregion

        #region IDisposable Implementation

        public void Dispose()
            => Handle.Dispose();

        #endregion
    }
}
