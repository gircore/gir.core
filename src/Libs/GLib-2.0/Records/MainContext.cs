using System;

namespace GLib
{
    public partial class MainContext
    {
        public static MainContext New()
            => new MainContext(Internal.MainContext.Methods.New());

        public static MainContext Default()
            => new MainContext(Internal.MainContext.Methods.Default());
    }
}
