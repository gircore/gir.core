using System;
using System.Threading.Tasks;

namespace Gtk;

public partial class FileLauncher
{
    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<bool> LaunchAsync(Window parent)
    {
        var tcs = new TaskCompletionSource<bool>();

        var callbackHandler = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
                return;
            }

            var launchValue = Internal.FileLauncher.LaunchFinish(sourceObject.Handle.DangerousGetHandle(), res.Handle.DangerousGetHandle(), out var error);

            if (!error.IsInvalid)
                tcs.SetException(new GLib.GException(error));
            else
                tcs.SetResult(launchValue);
        });

        Internal.FileLauncher.Launch(
            self: Handle.DangerousGetHandle(),
            parent: parent.Handle.DangerousGetHandle(),
            cancellable: IntPtr.Zero,
            callback: callbackHandler.NativeCallback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }

    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<bool> OpenContainingFolderAsync(Window parent)
    {
        var tcs = new TaskCompletionSource<bool>();

        var callbackHandler = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
                return;
            }

            var launchValue = Internal.FileLauncher.OpenContainingFolderFinish(sourceObject.Handle.DangerousGetHandle(), res.Handle.DangerousGetHandle(), out var error);

            if (!error.IsInvalid)
                tcs.SetException(new GLib.GException(error));
            else
                tcs.SetResult(launchValue);
        });

        Internal.FileLauncher.OpenContainingFolder(
            self: Handle.DangerousGetHandle(),
            parent: parent.Handle.DangerousGetHandle(),
            cancellable: IntPtr.Zero,
            callback: callbackHandler.NativeCallback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }
}
