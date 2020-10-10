using System;

namespace Gtk
{
    public partial class Global
    {
        public static void Init()
        {
            var argc = 0;
            var argv = IntPtr.Zero;

            
            Global.init(ref argc, ref argv);
        }

        public static void Main() => Global.main();
    }
}