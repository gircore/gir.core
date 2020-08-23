using System;

namespace WebKit2WebExtension
{
    public partial class WebPage
    {
        public event EventHandler<EventArgs>? DocumentLoaded;

        protected void OnDocumentLoaded() => DocumentLoaded?.Invoke(this, EventArgs.Empty); 
    }
}