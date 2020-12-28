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
            => new MainContext(@new());
        
        public static MainContext Default()
            => new MainContext(@default());
    }
}
