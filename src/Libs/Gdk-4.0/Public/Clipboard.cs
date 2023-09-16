using System;
using System.Threading.Tasks;

namespace Gdk;

public partial class Clipboard
{
    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<string?> ReadTextAsync()
    {
        var tcs = new TaskCompletionSource<string?>();

        var callbackHandler = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
                return;
            }

            var readValue = Internal.Clipboard.ReadTextFinish(sourceObject.Handle, res.Handle, out var error);

            if (!error.IsInvalid)
                tcs.SetException(new GLib.GException(error));
            else
                tcs.SetResult(readValue.ConvertToString());
        });

        Internal.Clipboard.ReadTextAsync(
            clipboard: Handle,
            cancellable: IntPtr.Zero,
            callback: callbackHandler.NativeCallback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }
}
