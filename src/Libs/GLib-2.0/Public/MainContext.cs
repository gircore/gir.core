using System;

namespace GLib
{
    public partial class MainContext
    {
        public static MainContext New()
            => new(Internal.MainContext.New());

        public static MainContext Default()
            => new(Internal.MainContext.Default());
    }
}
