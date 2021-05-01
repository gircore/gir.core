using System;

namespace WebKit2
{
    public partial class WebContext
    {
        public WebContext() : this(Sys.WebContext.@new()) { }

        public void ClearCache() => Sys.WebContext.clear_cache(Handle);
        public void SetWebExtensionsDirectory(string directory) => Sys.WebContext.set_web_extensions_directory(Handle, directory);
    }
}
