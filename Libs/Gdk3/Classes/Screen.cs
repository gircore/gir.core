using System;
using GObject;
using System.Runtime.InteropServices;

namespace Gdk
{   
    public partial class Screen
    {
        public static Gdk.Screen GetDefault()
            => WrapPointerAs<Gdk.Screen>(Native.get_default());
    }
}
