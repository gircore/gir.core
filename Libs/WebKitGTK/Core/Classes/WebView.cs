using System;
using System.Threading.Tasks;
using GObject;
using Gtk;
using JavaScriptCore;

namespace WebKit2
{
    public class WebView : Container
    {
        #region Properties
        public Property<WebContext?> Context { get; }
        #endregion Properties

        public WebView(WebContext context) : this(Sys.WebView.new_with_context(context)) {}
        public WebView() : this(Sys.WebView.@new()) { }

        internal WebView(IntPtr handle) : base(handle) 
        { 
            Context = Property("web-context",
                get : GetObject<WebContext?>,
                set : Set
            );
        }

        public void LoadUri(string uri) => Sys.WebView.load_uri(this, uri);
        public Settings GetSettings() => Convert(Sys.WebView.get_settings(this), (ptr) => new Settings(ptr, true));
        public UserContentManager GetUserContentManager() => Convert(Sys.WebView.get_user_content_manager(this), (ptr) => new UserContentManager(ptr, true));
        public WebInspector GetInspector() => Convert(Sys.WebView.get_inspector(this), (ptr) => new WebInspector(ptr, true));

        public Task<Value> RunJavascriptAsync(string script)
        {
            var tcs = new TaskCompletionSource<Value>();

            void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
            {
                var jsResult = Sys.WebView.run_javascript_finish(sourceObject, res, out var error);
                HandleError(error);

                var jsValue = Sys.JavascriptResult.get_js_value(jsResult);
                if (!TryGetObject<Value>(jsValue, out var value)) value = new Value(jsValue);

                tcs.SetResult(value);
            }

            Sys.WebView.run_javascript(this, script, IntPtr.Zero, Callback, IntPtr.Zero);

            return tcs.Task;
        }
    }
}