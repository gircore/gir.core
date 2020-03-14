using System;

namespace WebKitGTK.Core
{
    public class WebInspector : GObject.Core.GObject
    {
        internal WebInspector(IntPtr handle) : base(handle) { }

        public void Show() => WebKit2.WebInspector.show(this);  
    }
}