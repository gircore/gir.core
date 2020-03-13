using System;

namespace WebKit2WebExtension.Core
{
    public class WebPage : GObject.Core.GObject
    {
        public event EventHandler<EventArgs>? DocumentLoaded;

        internal WebPage(IntPtr handle) : base(handle) 
        {
            RegisterEvent("document-loaded", OnDocumentLoaded);
        }

        protected void OnDocumentLoaded() => DocumentLoaded?.Invoke(this, EventArgs.Empty); 
    }
}