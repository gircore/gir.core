using System;

namespace Gdk
{
    public partial class Screen
    {
        public static Screen? GetDefault()
        {
            IntPtr ptr = Internal.Screen.GetDefault();
            return GObject.Internal.ObjectWrapper.WrapNullableHandle<Screen>(ptr, false);
        }
    }
}
