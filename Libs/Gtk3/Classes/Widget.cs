using System;
using Type = GObject.Type;

namespace Gtk
{
    public partial class Widget
    {
        #region Methods

        private static void ClassInit(Type gClass, System.Type type, IntPtr classData)
        {
            Console.WriteLine("Class init Widget");
        }
        
        public void ShowAll() => Native.show_all(Handle);
        #endregion
    }
}
