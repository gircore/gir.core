using System;

namespace WebKit2WebExtension
{
    public class PageCreatedEventArgs : EventArgs
    {
        public WebPage WebPage { get; }

        public PageCreatedEventArgs(WebPage webPage)
        {
            WebPage = webPage ?? throw new ArgumentNullException(nameof(webPage));
        }
    }
    public partial class WebExtension : GObject.Object
    {
        public event EventHandler<PageCreatedEventArgs>? PageCreated;

        private WebPage GetPage(ref GObject.Sys.Value[] values) => null!;//TODO
        protected void OnPageCreated(ref GObject.Sys.Value[] values) => PageCreated?.Invoke(this, new PageCreatedEventArgs(GetPage(ref values)));
    }
}