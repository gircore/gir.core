using Gtk;
using System;

namespace Gtk.Core
{
    public partial class GApplication
    {
        public static void Init()
        {
            var argc = 0;
            var argv = IntPtr.Zero;

            Methods.init(ref argc, ref argv);
        }

        public static void Main() =>  Methods.main();
    }
}