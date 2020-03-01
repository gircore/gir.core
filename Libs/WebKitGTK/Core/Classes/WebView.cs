using Gtk.Core;

namespace WebKitGTK.Core
{
    public class WebView : GContainer
    {
        public WebView() : base(WebKit2.WebView.@new())
        {

        }
    }
}