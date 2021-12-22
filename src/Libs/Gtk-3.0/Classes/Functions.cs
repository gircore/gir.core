using System;

namespace Gtk
{
    public class Functions
    {
        public static void Init()
        {
            var argc = IntPtr.Zero;
            Internal.Functions.Init(ref argc, new string[0]);
        }

        public static void Main() => Internal.Functions.Main();

        public static void MainQuit() => Internal.Functions.MainQuit();
    }
}
