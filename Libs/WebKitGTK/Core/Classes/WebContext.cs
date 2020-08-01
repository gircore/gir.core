using System;

namespace WebKit2
{
    public class WebContext : GObject.Object
    {
        public event EventHandler<EventArgs>? InitializeWebExtensions;

        public WebContext() : this(Sys.WebContext.@new()) {}
        internal WebContext(IntPtr handle) : base(handle) 
        { 
            RegisterEvent("initialize-web-extensions", OnInitializeWebExtensions);
        }

        public void ClearCache() => Sys.WebContext.clear_cache(this);
        public void SetWebExtensionsDirectory(string directory) => Sys.WebContext.set_web_extensions_directory(this, directory);

        protected void OnInitializeWebExtensions() => InitializeWebExtensions?.Invoke(this, EventArgs.Empty);
    }
}