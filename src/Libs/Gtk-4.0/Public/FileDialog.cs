using System;
using System.Threading.Tasks;

namespace Gtk;

public partial class FileDialog
{
    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<Gio.File?> OpenAsync(Window parent)
    {
        var tcs = new TaskCompletionSource<Gio.File?>();

        var callback = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
            }
            else
            {
                var fileValue = Internal.FileDialog.OpenFinish(sourceObject.Handle, res.Handle, out var error);

                if (!error.IsInvalid)
                    throw new GLib.GException(error);

                if (fileValue == IntPtr.Zero)
                {
                    tcs.SetResult(null);
                }
                else
                {
                    var value = new Gio.FileHelper(fileValue, true);
                    tcs.SetResult(value);
                }
            }
        });

        Internal.FileDialog.Open(
            self: Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: callback.NativeCallback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }

    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<Gio.File?> SaveAsync(Window parent)
    {
        var tcs = new TaskCompletionSource<Gio.File?>();

        var callback = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
            }
            else
            {
                var fileValue = Internal.FileDialog.SaveFinish(sourceObject.Handle, res.Handle, out var error);

                if (!error.IsInvalid)
                    throw new GLib.GException(error);

                if (fileValue == IntPtr.Zero)
                {
                    tcs.SetResult(null);
                }
                else
                {
                    var value = new Gio.FileHelper(fileValue, true);
                    tcs.SetResult(value);
                }
            }
        });

        Internal.FileDialog.Save(
            self: Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: callback.NativeCallback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }


    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<Gio.File?> SelectFolderAsync(Window parent)
    {
        var tcs = new TaskCompletionSource<Gio.File?>();

        var callback = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
            }
            else
            {
                var fileValue = Internal.FileDialog.SelectFolderFinish(sourceObject.Handle, res.Handle, out var error);

                if (!error.IsInvalid)
                    throw new GLib.GException(error);

                if (fileValue == IntPtr.Zero)
                {
                    tcs.SetResult(null);
                }
                else
                {
                    var value = new Gio.FileHelper(fileValue, true);
                    tcs.SetResult(value);
                }
            }
        });

        Internal.FileDialog.SelectFolder(
            self: Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: callback.NativeCallback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }
}
