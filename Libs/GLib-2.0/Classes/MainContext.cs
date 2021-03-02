using System;

namespace GLib
{
    public partial class MainContext : IHandle
    {
        public IntPtr Handle { get; private set; }

        private MainContext(IntPtr handle)
        {
            Handle = handle;
        }

        public static MainContext New()
            => new MainContext(Native.Methods.New());

        public static MainContext Default()
            => new MainContext(Native.Methods.Default());
    }
}
