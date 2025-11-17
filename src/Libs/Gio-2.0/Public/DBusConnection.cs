using System;
using System.Threading.Tasks;
using GLib;

namespace Gio;

public partial class DBusConnection
{
    public static DBusConnection Get(BusType busType)
    {
        var handle = Internal.Functions.BusGetSync(busType, IntPtr.Zero, out var error);

        if (!error.IsInvalid)
            throw new GException(error);

        return (DBusConnection) GObject.Internal.InstanceWrapper.WrapHandle<DBusConnection>(handle, true);
    }

    public Task<Variant> CallAsync(string busName, string objectPath, string interfaceName, string methodName,
        Variant? parameters = null)
    {
        var tcs = new TaskCompletionSource<Variant>();

        var callbackHandler = new Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.TrySetException(new Exception("Missing source object"));
                return;
            }

            var ret = Internal.DBusConnection.CallFinish(sourceObject.Handle.DangerousGetHandle(), res.Handle.DangerousGetHandle(), out var error);

            if (!error.IsInvalid)
                tcs.TrySetException(new GException(error));
            else
                tcs.TrySetResult(new Variant(ret));
        });

        Internal.DBusConnection.Call(Handle.DangerousGetHandle(),
            GLib.Internal.NullableUtf8StringOwnedHandle.Create(busName), GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(objectPath),
            GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(interfaceName), GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(methodName),
            (GLib.Internal.VariantHandle?) parameters?.Handle ?? GLib.Internal.VariantUnownedHandle.NullHandle, GLib.Internal.VariantTypeUnownedHandle.NullHandle, DBusCallFlags.None, -1, IntPtr.Zero, callbackHandler.NativeCallback, IntPtr.Zero);

        return tcs.Task;
    }
}
