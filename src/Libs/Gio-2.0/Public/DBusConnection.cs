using System;
using System.Threading.Tasks;
using GLib;

namespace Gio;

public partial class DBusConnection
{
    #region Static methods

    public static DBusConnection Get(BusType busType)
    {
        var handle = Internal.Functions.BusGetSync(busType, IntPtr.Zero, out var error);
        Error.ThrowOnError(error);

        return GObject.Internal.ObjectWrapper.WrapHandle<DBusConnection>(handle, true);
    }

    #endregion

    #region Methods

    public Task<Variant> CallAsync(string busName, string objectPath, string interfaceName, string methodName,
        Variant? parameters = null)
    {
        var tcs = new TaskCompletionSource<Variant>();

        void Callback(GObject.Object sourceObject, AsyncResult res)
        {
            // TODO: Make sure this is correct (can we assume res is a GObject?)
            var ret = Internal.DBusConnection.CallFinish(sourceObject.Handle, (res as GObject.Object).Handle, out var error);
            Error.ThrowOnError(error);

            tcs.SetResult(new Variant(ret));
        }

        var callAsyncCallbackHandler = new Internal.AsyncReadyCallbackAsyncHandler(Callback);

        Internal.DBusConnection.Call(Handle,
            GLib.Internal.NullableUtf8StringOwnedHandle.Create(busName), GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(objectPath),
            GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(interfaceName), GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(methodName),
            parameters.GetSafeHandle(), GLib.Internal.VariantTypeNullHandle.Instance, DBusCallFlags.None, -1, IntPtr.Zero, callAsyncCallbackHandler.NativeCallback, IntPtr.Zero);

        return tcs.Task;
    }

    public Variant Call(string busName, string objectPath, string interfaceName, string methodName, Variant? parameters = null)
    {
        var parameterHandle = parameters?.Handle ?? GLib.Internal.VariantNullHandle.Instance;
        var ret = Internal.DBusConnection.CallSync(Handle,
            GLib.Internal.NullableUtf8StringOwnedHandle.Create(busName), GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(objectPath),
            GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(interfaceName), GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(methodName),
            parameterHandle, GLib.Internal.VariantTypeNullHandle.Instance, DBusCallFlags.None, 9999, IntPtr.Zero, out var error);

        Error.ThrowOnError(error);

        return new Variant(ret);
    }

    #endregion
}
