using System;

namespace WebKit2
{
    public partial class WebInspector
    {
        public void Show() => Sys.WebInspector.show(Handle);  
    }
}