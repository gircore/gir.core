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

        return GObject.Internal.ObjectWrapper.WrapHandle<DBusConnection>(handle, true);
    }

    public Task<Variant> CallAsync(string busName, string objectPath, string interfaceName, string methodName,
        Variant? parameters = null)
    {
        var tcs = new TaskCompletionSource<Variant>();

        var callbackHandler = new Internal.AsyncReadyCallbackAsyncHandler((sourceObject, res, data) =>
        {
            if (sourceObject is null)
            {
                tcs.SetException(new Exception("Missing source object"));
                return;
            }

            var ret = Internal.DBusConnection.CallFinish(sourceObject.Handle, res.Handle, out var error);

            if (!error.IsInvalid)
                tcs.SetException(new GException(error));
            else
                tcs.SetResult(new Variant(ret));
        });

        Internal.DBusConnection.Call(Handle,
            GLib.Internal.NullableUtf8StringOwnedHandle.Create(busName), GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(objectPath),
            GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(interfaceName), GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(methodName),
            parameters.GetSafeHandle(), GLib.Internal.VariantTypeNullHandle.Instance, DBusCallFlags.None, -1, IntPtr.Zero, callbackHandler.NativeCallback, IntPtr.Zero);

        return tcs.Task;
    }

    public Variant Call(string busName, string objectPath, string interfaceName, string methodName, Variant? parameters = null)
    {
        var parameterHandle = parameters?.Handle ?? GLib.Internal.VariantNullHandle.Instance;
        var ret = Internal.DBusConnection.CallSync(Handle,
            GLib.Internal.NullableUtf8StringOwnedHandle.Create(busName), GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(objectPath),
            GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(interfaceName), GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(methodName),
            parameterHandle, GLib.Internal.VariantTypeNullHandle.Instance, DBusCallFlags.None, 9999, IntPtr.Zero, out var error);

        if (!error.IsInvalid)
            throw new GException(error);

        return new Variant(ret);
    }
}
