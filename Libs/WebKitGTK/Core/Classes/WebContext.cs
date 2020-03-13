using System;

namespace WebKitGTK.Core
{
    public class WebContext : GObject.Core.GObject
    {
        public WebContext() : this(WebKit2.WebContext.@new()) {}
        internal WebContext(IntPtr handle) : base(handle) { }

        public void ClearCache() => WebKit2.WebContext.clear_cache(this);
    }
}