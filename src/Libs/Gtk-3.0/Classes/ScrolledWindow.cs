using System;
using GObject.Internal;

namespace Gtk
{
    public partial class ScrolledWindow
    {
        public static ScrolledWindow New(Adjustment? hAdjustment = null, Adjustment? vAdjustment = null)
        {
            IntPtr ptr = Internal.ScrolledWindow.Instance.Methods.New(
                hAdjustment?.Handle ?? IntPtr.Zero,
                vAdjustment?.Handle ?? IntPtr.Zero
            );

            return new(ptr, false);
        }
    }
}
