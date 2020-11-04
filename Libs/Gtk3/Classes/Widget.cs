using System;

namespace Gtk
{
    public partial class Widget
    {
        #region Methods

        private static void ClassInit(IntPtr gClass, IntPtr classData)
        {
            Console.WriteLine("Class init Widget");
        }
        
        public void ShowAll() => Native.show_all(Handle);
        #endregion
    }
}
