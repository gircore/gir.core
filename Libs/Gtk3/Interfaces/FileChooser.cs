using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GObject;

namespace Gtk
{
    public partial interface FileChooser
    {
        public string? GetUri()
        {
            var obj = (GObject.Object) this;
            var ptr = Native.get_uri(obj.Handle);
            return Marshal.PtrToStringAnsi(ptr);
        }
    }
}
