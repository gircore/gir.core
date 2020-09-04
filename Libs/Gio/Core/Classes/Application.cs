using System;
using System.Collections.Generic;
using GObject;

namespace Gio
{
    public partial class Application
    {
        public Application(string applicationId) : this(Sys.Application.@new(applicationId, Sys.ApplicationFlags.flags_none)) { }

        public void Run()
        {
            var zero = IntPtr.Zero;
            Sys.Application.run(Handle, 0, ref zero);
        }
    }
}
