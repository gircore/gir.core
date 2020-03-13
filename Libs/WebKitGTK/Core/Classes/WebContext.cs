using System;

namespace WebKitGTK.Core
{
    public class WebContext : GObject.Core.GObject
    {
        public event EventHandler<EventArgs>? InitializeWebExtensions;

        public WebContext() : this(WebKit2.WebContext.@new()) {}
        internal WebContext(IntPtr handle) : base(handle) 
        { 
            RegisterEvent("initialize-web-extensions", OnInitializeWebExtensions);
        }

        public void ClearCache() => WebKit2.WebContext.clear_cache(this);
        public void SetWebExtensionsDirectory(string directory) => WebKit2.WebContext.set_web_extensions_directory(this, directory);

        protected void OnInitializeWebExtensions() => InitializeWebExtensions?.Invoke(this, EventArgs.Empty);
    }
}