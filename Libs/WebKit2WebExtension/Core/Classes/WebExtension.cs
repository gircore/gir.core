using System;

namespace WebKit2WebExtension.Core
{
    public class PageCreatedEventArgs : EventArgs
    {
        public WebPage WebPage { get; }

        public PageCreatedEventArgs(WebPage webPage)
        {
            WebPage = webPage ?? throw new ArgumentNullException(nameof(webPage));
        }
    }
    public class WebExtension : GObject.Core.GObject
    {
        public event EventHandler<PageCreatedEventArgs>? PageCreated;
        internal WebExtension(IntPtr handle) : base(handle) 
        {
            RegisterEvent("page-created", OnPageCreated);
        }

        private WebPage GetPage(ref GObject.Value[] values) => (WebPage)(GObject.Core.GObject)(IntPtr)values[1]!;
        protected void OnPageCreated(ref GObject.Value[] values) => PageCreated?.Invoke(this, new PageCreatedEventArgs(GetPage(ref values)));
    }
}