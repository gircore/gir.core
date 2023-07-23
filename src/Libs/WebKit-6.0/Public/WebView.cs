using System;
using System.Threading.Tasks;

namespace WebKit;

public partial class WebView
{
    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<JavaScriptCore.Value> EvaluateJavascriptAsync(string script)
    {
        var tcs = new TaskCompletionSource<JavaScriptCore.Value>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var jsValue = Internal.WebView.EvaluateJavascriptFinish(sourceObject, res, out var error);

            if (!error.IsInvalid)
                throw new GLib.GException(error);

            var value = GObject.Internal.ObjectWrapper.WrapHandle<JavaScriptCore.Value>(jsValue, true);
            tcs.SetResult(value);
        }

        Internal.WebView.EvaluateJavascript(
            webView: Handle,
            script: GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(script),
            length: -1,
            worldName: GLib.Internal.NullableUtf8StringOwnedHandle.Create(null),
            sourceUri: GLib.Internal.NullableUtf8StringOwnedHandle.Create(null),
            cancellable: IntPtr.Zero,
            callback: Callback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }
}
