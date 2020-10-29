using System;
using GObject;
using System.Runtime.InteropServices;

namespace Gdk
{   
    public partial class Screen
    {
        [Obsolete("Not actually supported - quick typedict hack")]
        public Screen() {}

        public static Gdk.Screen GetDefault()
            => WrapPointerAs<Gdk.Screen>(Native.get_default());
    }
}
