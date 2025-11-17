using System;
using System.Threading.Tasks;

namespace WebKit;

public partial class WebView
{
    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<JavaScriptCore.Value> EvaluateJavascriptAsync(string script)
    {
        var tcs = new TaskCompletionSource<JavaScriptCore.Value>();

        var callbackHandler = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.TrySetException(new Exception("Missing source object"));
                return;
            }

            var jsValue = Internal.WebView.EvaluateJavascriptFinish(sourceObject.Handle.DangerousGetHandle(), res.Handle.DangerousGetHandle(), out var error);

            if (!error.IsInvalid)
                tcs.TrySetException(new GLib.GException(error));
            else
                tcs.TrySetResult((JavaScriptCore.Value) GObject.Internal.InstanceWrapper.WrapHandle<JavaScriptCore.Value>(jsValue, true));
        });

        Internal.WebView.EvaluateJavascript(
            webView: Handle.DangerousGetHandle(),
            script: GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(script),
            length: -1,
            worldName: GLib.Internal.NullableUtf8StringOwnedHandle.Create(null),
            sourceUri: GLib.Internal.NullableUtf8StringOwnedHandle.Create(null),
            cancellable: IntPtr.Zero,
            callback: callbackHandler.NativeCallback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }
}
