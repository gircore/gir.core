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

            Native.init(ref argc, ref argv);
        }

        public static void Main() => Native.main();

        public static void MainQuit() => Native.main_quit();

        #endregion
    }
}
