using System;
using System.Threading.Tasks;

namespace Gtk;

public partial class FontDialog
{
    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<Pango.FontFace?> ChooseFaceAsync(Window parent, Pango.FontFace? fontFace)
    {
        var tcs = new TaskCompletionSource<Pango.FontFace?>();

        var callbackHandler = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
                return;
            }

            var chooseFontFaceResult = Internal.FontDialog.ChooseFaceFinish(sourceObject.Handle, res.Handle, out var error);

            if (!error.IsInvalid)
                tcs.SetException(new GLib.GException(error));
            else
            {
                var result = GObject.Internal.ObjectWrapper.WrapNullableHandle<Pango.FontFace>(chooseFontFaceResult, false);
                tcs.SetResult(result);
            }
        });

        Internal.FontDialog.ChooseFace(
            Handle,
            parent.Handle,
            fontFace?.Handle ?? IntPtr.Zero,
            IntPtr.Zero,
            callbackHandler.NativeCallback,
            IntPtr.Zero
        );

        return tcs.Task;
    }

    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<Pango.FontFamily?> ChooseFamilyAsync(Window parent, Pango.FontFamily? fontFamily)
    {
        var tcs = new TaskCompletionSource<Pango.FontFamily?>();

        var callbackHandler = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
                return;
            }

            var chooseFontFamilyResult = Internal.FontDialog.ChooseFamilyFinish(sourceObject.Handle, res.Handle, out var error);

            if (!error.IsInvalid)
                tcs.SetException(new GLib.GException(error));
            else
            {
                var result = GObject.Internal.ObjectWrapper.WrapNullableHandle<Pango.FontFamily>(chooseFontFamilyResult, false);
                tcs.SetResult(result);
            }
        });

        Internal.FontDialog.ChooseFamily(
            Handle,
            parent.Handle,
            fontFamily?.Handle ?? IntPtr.Zero,
            IntPtr.Zero,
            callbackHandler.NativeCallback,
            IntPtr.Zero
        );

        return tcs.Task;
    }

    //TODO: Async methods should be generated automatically (https://github.com/gircore/gir.core/issues/893)
    public Task<Pango.FontDescription?> ChooseFontAsync(Window parent, Pango.FontDescription? fontDescription)
    {
        var tcs = new TaskCompletionSource<Pango.FontDescription?>();

        var callbackHandler = new Gio.Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
                return;
            }

            var fontDescriptionOwnedHandle = Internal.FontDialog.ChooseFontFinish(sourceObject.Handle, res.Handle, out var error);

            if (!error.IsInvalid)
                tcs.SetException(new GLib.GException(error));
            else
            {
                if (fontDescriptionOwnedHandle.IsInvalid)
                    tcs.SetResult(null);
                else
                {
                    var result = new Pango.FontDescription(fontDescriptionOwnedHandle);
                    tcs.SetResult(result);
                }
            }
        });

        var initialValue = (Pango.Internal.FontDescriptionHandle?) fontDescription?.Handle ?? Pango.Internal.FontDescriptionUnownedHandle.NullHandle;
        Internal.FontDialog.ChooseFont(
            Handle,
            parent.Handle,
            initialValue,
            IntPtr.Zero,
            callbackHandler.NativeCallback,
            IntPtr.Zero
        );

        return tcs.Task;
    }
}
