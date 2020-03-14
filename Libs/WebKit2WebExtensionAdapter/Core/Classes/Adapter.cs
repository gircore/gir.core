using System;
using WebExtensionAdapter;

namespace WebKit2WebExtension.Core
{
    public class WebExtensionInitializedEventArgs : EventArgs
    {
        public WebExtension WebExtension { get; }

        public WebExtensionInitializedEventArgs(WebExtension webExtension)
        {
            WebExtension = webExtension ?? throw new ArgumentNullException(nameof(webExtension));
        }
    }

    public class Adapter : GObject.Core.GObject
    {
        public event EventHandler<WebExtensionInitializedEventArgs>? Initialized;
        public Adapter() : this(WebExtensionAdapter.Adapter.@new()) 
        {
            
        }

        public Adapter(IntPtr handle) : base(handle)
        {
            RegisterEvent("web-extension-initialize", OnInitialized);
        }

        public static Adapter Init() =>  new Adapter(Methods.init());
        public void Activate() => Methods.set_adapter(this);
        public void Print() => WebExtensionAdapter.Adapter.printit();

        private WebExtension GetWebExtension(ref GObject.Value[] values) => ((WebExtension?)(GObject.Core.GObject?)(IntPtr)values[1])!;
        protected void OnInitialized(ref GObject.Value[] values) => Initialized?.Invoke(this, new WebExtensionInitializedEventArgs(GetWebExtension(ref values)));
    }
}