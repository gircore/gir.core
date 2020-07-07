using System;

namespace GLib.Core
{
    public partial class GVariantType
    {
        public static GVariantType String = new GVariantType("s");
        public static GVariantType Variant = new GVariantType("v");
       
    }
}