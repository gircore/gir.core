using System;

namespace GLib
{
    public partial class VariantType
    {
        private readonly IntPtr handle;
        internal IntPtr Handle => handle;

        public VariantType(string type) : this(VariantType.@new(type)) {}

        internal VariantType(IntPtr handle)
        {
            this.handle = handle;
        }
       
    }
}