using System.Runtime.InteropServices;
using GObject;

namespace Gtk
{
    //TODO: Generate this automatically
    public static class EnumHelper
    {
        public static Type GetOrientationType()
            => new Type(Native.gtk_orientation_get_type());

        private static class Native
        {
            [DllImport("Gtk", EntryPoint = "gtk_orientation_get_type")]
            public static extern ulong gtk_orientation_get_type();
        }
    }
}
