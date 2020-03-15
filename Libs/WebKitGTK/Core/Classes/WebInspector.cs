using System;

namespace WebKitGTK.Core
{
    public class WebInspector : GObject.Core.GObject
    {
        internal WebInspector(IntPtr handle, bool isInitiallyUnowned = false) : base(handle, isInitiallyUnowned) { }

        public void Show() => WebKit2.WebInspector.show(this);  
    }
}