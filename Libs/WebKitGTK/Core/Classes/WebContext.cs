using System;

namespace WebKit2
{
    public partial class WebContext
    {
        public event EventHandler<EventArgs>? InitializeWebExtensions;

        public WebContext() : this(Sys.WebContext.@new()) {}

        public void ClearCache() => Sys.WebContext.clear_cache(Handle);
        public void SetWebExtensionsDirectory(string directory) => Sys.WebContext.set_web_extensions_directory(Handle, directory);

        protected void OnInitializeWebExtensions() => InitializeWebExtensions?.Invoke(this, EventArgs.Empty);
    }
}