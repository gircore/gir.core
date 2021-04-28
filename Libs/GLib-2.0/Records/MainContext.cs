using System;

namespace GLib
{
    public partial record MainContext
    {
        public Native.MainContext.Handle Handle { get; private set; }

        private MainContext(Native.MainContext.Handle handle)
        {
            Handle = handle;
        }

        public static MainContext New()
            => new MainContext(Native.MainContext.Methods.New());

        public static MainContext Default()
            => new MainContext(Native.MainContext.Methods.Default());
    }
}
