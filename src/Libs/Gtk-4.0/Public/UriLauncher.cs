using System;
using System.Threading.Tasks;

namespace Gtk;

public partial class UriLauncher
{
    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<bool> LaunchAsync(Window parent)
    {
        var tcs = new TaskCompletionSource<bool>();

        var callback = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
            }
            else
            {
                var launchValue = Internal.UriLauncher.LaunchFinish(sourceObject.Handle, res.Handle, out var error);

                if (!error.IsInvalid)
                    throw new GLib.GException(error);

                tcs.SetResult(launchValue);
            }
        });

        Internal.UriLauncher.Launch(
            self: Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: callback.NativeCallback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }
}
