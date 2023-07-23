using System;
using System.Threading.Tasks;

namespace Gtk;

public partial class FileDialog
{
    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<Gio.File?> OpenAsync(Window parent)
    {
        var tcs = new TaskCompletionSource<Gio.File?>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var fileValue = Internal.FileDialog.OpenFinish(sourceObject, res, out var error);

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

        Internal.FileDialog.Open(
            self: Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: Callback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }

    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<Gio.File?> SaveAsync(Window parent)
    {
        var tcs = new TaskCompletionSource<Gio.File?>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var fileValue = Internal.FileDialog.SaveFinish(sourceObject, res, out var error);

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

        Internal.FileDialog.Save(
            self: Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: Callback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }


    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<Gio.File?> SelectFolderAsync(Window parent)
    {
        var tcs = new TaskCompletionSource<Gio.File?>();

        void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
        {
            var fileValue = Internal.FileDialog.SelectFolderFinish(sourceObject, res, out var error);

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

        Internal.FileDialog.SelectFolder(
            self: Handle,
            parent: parent.Handle,
            cancellable: IntPtr.Zero,
            callback: Callback,
            userData: IntPtr.Zero
        );

        return tcs.Task;
    }
}
