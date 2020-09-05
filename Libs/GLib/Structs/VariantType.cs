using System;

namespace GLib
{
    public partial struct VariantType : IDisposable
    {
        public static VariantType String = Create("s");
        public static VariantType Variant = Create("v");

        public static VariantType Create(string type) => VariantType.@new(type);
        
        public void Dispose()
            => VariantType.free(ref this);
    }
}