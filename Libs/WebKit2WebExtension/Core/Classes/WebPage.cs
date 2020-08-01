using System;

namespace WebKit2WebExtension
{
    public class WebPage : GObject.Object
    {
        public event EventHandler<EventArgs>? DocumentLoaded;

        internal WebPage(IntPtr handle) : base(handle) 
        {
            RegisterEvent("document-loaded", OnDocumentLoaded);
        }

        protected void OnDocumentLoaded() => DocumentLoaded?.Invoke(this, EventArgs.Empty); 
    }
}