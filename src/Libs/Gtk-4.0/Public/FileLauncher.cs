using System;
using System.Threading.Tasks;

namespace Gtk;

public partial class FileLauncher
{
    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<bool> LaunchAsync(Window parent)
    {
        var tcs = new TaskCompletionSource<bool>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var launchValue = Internal.FileLauncher.LaunchFinish(sourceObject, res, out var error);
            GLib.Error.ThrowOnError(error);

            tcs.SetResult(launchValue);
        }

        Internal.FileLauncher.Launch(
            self: Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: Callback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }

    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<bool> OpenContainingFolderAsync(Window parent)
    {
        var tcs = new TaskCompletionSource<bool>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var launchValue = Internal.FileLauncher.OpenContainingFolderFinish(sourceObject, res, out var error);
            GLib.Error.ThrowOnError(error);

            tcs.SetResult(launchValue);
        }

        Internal.FileLauncher.OpenContainingFolder(
            self: Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: Callback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }
}
