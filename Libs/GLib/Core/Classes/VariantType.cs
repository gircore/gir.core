using System;

namespace GLib.Core
{
    public partial class GVariantType
    {
        private readonly IntPtr handle;
        internal IntPtr Handle => handle;

        public GVariantType(string type) : this(VariantType.@new(type)) {}

        internal GVariantType(IntPtr handle)
        {
            this.handle = handle;
        }
       
    }
}