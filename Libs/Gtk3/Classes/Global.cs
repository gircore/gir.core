using System;

namespace Gtk
{
    public partial class Global
    {
        #region Methods

        public static void Init()
        {
            var argc = 0;
            IntPtr argv = IntPtr.Zero;

            init(ref argc, ref argv);
        }

        public static void Main() => main();

        public static void MainQuit() => main_quit();

        #endregion
    }
}