using System;
using System.Threading.Tasks;

namespace WebKit2;

public partial class WebView
{
    //TODO: Async methods should be generated automatically
    public Task<JavaScriptCore.Value> RunJavascriptAsync(string script)
    {
        var tcs = new TaskCompletionSource<JavaScriptCore.Value>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var jsResult = Internal.WebView.RunJavascriptFinish(sourceObject, res, out var error);
            GLib.Error.ThrowOnError(error);

            var jsValue = Internal.JavascriptResult.GetJsValue(jsResult);
            var value = GObject.Internal.ObjectWrapper.WrapHandle<JavaScriptCore.Value>(jsValue, false);
            tcs.SetResult(value);
        }

        Internal.WebView.RunJavascript(Handle, GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(script), IntPtr.Zero, Callback, IntPtr.Zero);

        return tcs.Task;
    }
}
