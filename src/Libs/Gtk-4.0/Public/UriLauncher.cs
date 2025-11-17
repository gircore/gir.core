using System;
using System.Threading.Tasks;

namespace Gtk;

public partial class UriLauncher
{
    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<bool> LaunchAsync(Window? parent)
    {
        var tcs = new TaskCompletionSource<bool>();

        var callbackHandler = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.TrySetException(new Exception("Missing source object"));
                return;
            }

            var launchValue = Internal.UriLauncher.LaunchFinish(sourceObject.Handle.DangerousGetHandle(), res.Handle.DangerousGetHandle(), out var error);

            if (!error.IsInvalid)
                tcs.TrySetException(new GLib.GException(error));
            else
                tcs.TrySetResult(launchValue);
        });

        Internal.UriLauncher.Launch(
            self: Handle.DangerousGetHandle(),
            parent: parent?.Handle.DangerousGetHandle() ?? IntPtr.Zero,
            cancellable: IntPtr.Zero,
            callback: callbackHandler.NativeCallback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }
}
