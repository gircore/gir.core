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

            var chooseFontFaceResult = Internal.FontDialog.ChooseFaceFinish(sourceObject.Handle.DangerousGetHandle(), res.Handle.DangerousGetHandle(), out var error);

            if (!error.IsInvalid)
                tcs.SetException(new GLib.GException(error));
            if (chooseFontFaceResult == IntPtr.Zero)
                tcs.SetResult(null);
            else
                tcs.SetResult((Pango.FontFace) GObject.Internal.InstanceWrapper.WrapHandle<Pango.FontFace>(chooseFontFaceResult, true));
        });

        Internal.FontDialog.ChooseFace(
            Handle.DangerousGetHandle(),
            parent.Handle.DangerousGetHandle(),
            fontFace?.Handle.DangerousGetHandle() ?? IntPtr.Zero,
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

            var chooseFontFamilyResult = Internal.FontDialog.ChooseFamilyFinish(sourceObject.Handle.DangerousGetHandle(), res.Handle.DangerousGetHandle(), out var error);

            if (!error.IsInvalid)
                tcs.SetException(new GLib.GException(error));
            else if (chooseFontFamilyResult == IntPtr.Zero)
                tcs.SetResult(null);
            else
                tcs.SetResult((Pango.FontFamily) GObject.Internal.InstanceWrapper.WrapHandle<Pango.FontFamily>(chooseFontFamilyResult, true));
        });

        Internal.FontDialog.ChooseFamily(
            Handle.DangerousGetHandle(),
            parent.Handle.DangerousGetHandle(),
            fontFamily?.Handle.DangerousGetHandle() ?? IntPtr.Zero,
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

            var fontDescriptionOwnedHandle = Internal.FontDialog.ChooseFontFinish(sourceObject.Handle.DangerousGetHandle(), res.Handle.DangerousGetHandle(), out var error);

            if (!error.IsInvalid)
                tcs.SetException(new GLib.GException(error));
            else if (fontDescriptionOwnedHandle.IsInvalid)
                tcs.SetResult(null);
            else
                tcs.SetResult(new Pango.FontDescription(fontDescriptionOwnedHandle));
        });

        var initialValue = (Pango.Internal.FontDescriptionHandle?) fontDescription?.Handle ?? Pango.Internal.FontDescriptionUnownedHandle.NullHandle;
        Internal.FontDialog.ChooseFont(
            Handle.DangerousGetHandle(),
            parent.Handle.DangerousGetHandle(),
            initialValue,
            IntPtr.Zero,
            callbackHandler.NativeCallback,
            IntPtr.Zero
        );

        return tcs.Task;
    }
}
