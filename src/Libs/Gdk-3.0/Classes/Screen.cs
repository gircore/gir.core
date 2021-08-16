using System;

namespace Gdk
{
    public partial class Screen
    {
        public static Screen? GetDefault()
        {
            IntPtr ptr = Native.Screen.Instance.Methods.GetDefault();
            return GObject.Native.ObjectWrapper.WrapNullableHandle<Screen>(ptr, false);
        }
    }
}
