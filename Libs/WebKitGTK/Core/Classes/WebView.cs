using Gtk.Core;

namespace WebKitGTK.Core
{
    public class WebView : GContainer
    {
        public WebView() : base(WebKit2.WebView.@new()) { }

        public void LoadUri(string uri) => WebKit2.WebView.load_uri(this, uri);
    }
}