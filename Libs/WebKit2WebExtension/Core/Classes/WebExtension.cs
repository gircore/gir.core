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
    public class WebExtension : GObject.Object
    {
        public event EventHandler<PageCreatedEventArgs>? PageCreated;
        internal WebExtension(IntPtr handle) : base(handle) 
        {
            RegisterEvent("page-created", OnPageCreated);
        }

        private WebPage GetPage(ref GObject.Sys.Value[] values) => ((WebPage?)(GObject.Object?)(IntPtr)values[1])!;
        protected void OnPageCreated(ref GObject.Sys.Value[] values) => PageCreated?.Invoke(this, new PageCreatedEventArgs(GetPage(ref values)));
    }
}