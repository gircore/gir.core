using System;
using GObject.Core;
using Gtk.Core;
using JavaScriptCore.Core;

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
        public Settings GetSettings() => Convert(WebKit2.WebView.get_settings(this), (ptr) => new Settings(ptr, true));
        public UserContentManager GetUserContentManager() => Convert(WebKit2.WebView.get_user_content_manager(this), (ptr) => new UserContentManager(ptr, true));
        public WebInspector GetInspector() => Convert(WebKit2.WebView.get_inspector(this), (ptr) => new WebInspector(ptr, true));

        public void RunJavascript(string script, Action<Value> callback)
        {
            Gio.AsyncReadyCallback cb = (sourceObject, res, userData) =>
            {
                var view = (WebView)(GObject.Core.GObject)sourceObject!;
                var value = view.FinishJavascript(res);
                callback(value);
            };

            WebKit2.WebView.run_javascript(this, script, IntPtr.Zero, cb, IntPtr.Zero);
        }

        protected Value FinishJavascript(IntPtr result)
        {
            var jsResult = WebKit2.WebView.run_javascript_finish(this, result, out var error);
            HandleError(error);

            var jsValue = WebKit2.JavascriptResult.get_js_value(jsResult);
            if(!TryGetObject<Value>(jsValue, out var value))
                value = new Value(jsValue);

            return value!;
        }
    }
}