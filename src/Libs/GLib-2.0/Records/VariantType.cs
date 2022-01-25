﻿using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial class VariantType : IDisposable
    {
        #region Static Member

        public static readonly VariantType String = new VariantType("s");
        public static readonly VariantType Variant = new VariantType("v");

        #endregion

        #region Constructors

        public VariantType(string type) : this(Internal.VariantType.New(type)) { }

        #endregion

        #region Methods

        public override string? ToString()
            => Marshal.PtrToStringAnsi(Internal.VariantType.PeekString(Handle));

        #endregion

        #region IDisposable Implementation

        public void Dispose()
            => Handle.Dispose();

        #endregion
    }
}
