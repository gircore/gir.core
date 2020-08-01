using System;

namespace WebKit2
{
    public class WebInspector : GObject.Object
    {
        internal WebInspector(IntPtr handle, bool isInitiallyUnowned = false) : base(handle, isInitiallyUnowned) { }

        public void Show() => Sys.WebInspector.show(this);  
    }
}