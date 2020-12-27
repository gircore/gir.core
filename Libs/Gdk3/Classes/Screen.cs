using System;
using GObject;
using System.Runtime.InteropServices;

namespace Gdk
{   
    public partial class Screen
    {
        public static Gdk.Screen GetDefault()
            => WrapHandle<Gdk.Screen>(Native.get_default());
    }
}
