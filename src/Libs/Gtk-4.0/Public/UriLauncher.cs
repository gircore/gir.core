using System;
using System.Threading.Tasks;

namespace Gtk;

public partial class UriLauncher
{
    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<bool> LaunchAsync(Window parent)
    {
        var tcs = new TaskCompletionSource<bool>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var launchValue = Internal.UriLauncher.LaunchFinish(sourceObject, res, out var error);
            GLib.Error.ThrowOnError(error);

            tcs.SetResult(launchValue);
        }

        Internal.UriLauncher.Launch(
            self: Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: Callback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }
}
