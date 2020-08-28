using System;
using System.Threading.Tasks;
using GObject;
using Gtk;
using JavaScriptCore;

namespace WebKit2
{
    public partial class WebView
    {
        #region Properties
        public IProperty<WebContext?> Context { get; }
        #endregion Properties

        public WebView(WebContext context) : this(Sys.WebView.new_with_context(GetHandle(context))) { }

        public void LoadUri(string uri) => Sys.WebView.load_uri(Handle, uri);
        public Settings GetSettings() => WrapPointerAs<Settings>(Sys.WebView.get_settings(Handle));
        public UserContentManager GetUserContentManager() => WrapPointerAs<UserContentManager>(Sys.WebView.get_user_content_manager(Handle));
        public WebInspector GetInspector() => WrapPointerAs<WebInspector>(Sys.WebView.get_inspector(Handle));

        public Task<Value> RunJavascriptAsync(string script)
        {
            var tcs = new TaskCompletionSource<Value>();

            void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
            {
                var jsResult = Sys.WebView.run_javascript_finish(sourceObject, res, out var error);
                HandleError(error);

                var jsValue = Sys.JavascriptResult.get_js_value(jsResult);
                var value = WrapPointerAs<Value>(jsValue);
                tcs.SetResult(value);
            }

            Sys.WebView.run_javascript(Handle, script, IntPtr.Zero, Callback, IntPtr.Zero);

            return tcs.Task;
        }
    }
}