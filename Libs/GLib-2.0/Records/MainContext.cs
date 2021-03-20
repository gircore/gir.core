using System;

namespace GLib
{
    public partial record MainContext
    {
        public Native.MainContextSafeHandle Handle { get; private set; }

        private MainContext(Native.MainContextSafeHandle handle)
        {
            Handle = handle;
        }

        public static MainContext New()
            => new MainContext(Native.Methods.New());

        public static MainContext Default()
            => new MainContext(Native.Methods.Default());
    }
}
