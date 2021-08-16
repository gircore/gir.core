using System;
using GObject.Native;

namespace Gtk
{
    public partial class ScrolledWindow
    {
        public static ScrolledWindow New(Adjustment? hAdjustment = null, Adjustment? vAdjustment = null)
        {
            IntPtr ptr = Native.ScrolledWindow.Instance.Methods.New(
                hAdjustment?.Handle ?? IntPtr.Zero,
                vAdjustment?.Handle ?? IntPtr.Zero
            );

            return new(ptr, false);
        }
    }
}
