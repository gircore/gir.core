using System;
using GObject.Core;
using Gtk.Core;

namespace WebKitGTK.Core
{
    public class WebView : GContainer
    {
        #region Properties
        public Property<WebContext?> Context { get; }
        #endregion Properties

        public WebView(WebContext context) : this(WebKit2.WebView.new_with_context(context)) {}
        public WebView() : this(WebKit2.WebView.@new()) { }

        internal WebView(IntPtr handle) : base(handle) 
        { 
            Context = Property<WebContext?>("web-context",
                get : GetObject<WebContext?>,
                set : Set
            );
        }

        public void LoadUri(string uri) => WebKit2.WebView.load_uri(this, uri);
        public Settings GetSettings() => Convert(WebKit2.WebView.get_settings(this), (ptr) => new Settings(ptr));
        public UserContentManager GetUserContentManager() => Convert(WebKit2.WebView.get_user_content_manager(this), (ptr) => new UserContentManager(ptr));
        public WebInspector GetInspector() => Convert(WebKit2.WebView.get_inspector(this), (ptr) => new WebInspector(ptr));
    }
}