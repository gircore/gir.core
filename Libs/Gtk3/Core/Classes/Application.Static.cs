using System;

namespace Gtk
{
    public partial class Application
    {
        public static void Init()
        {
            var argc = 0;
            var argv = IntPtr.Zero;

            Sys.Methods.init(ref argc, ref argv);
        }

        public static void Main() => Sys.Methods.main();
    }
}