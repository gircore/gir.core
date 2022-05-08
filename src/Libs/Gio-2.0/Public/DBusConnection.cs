using System;
using System.Threading.Tasks;
using GLib;

namespace Gio
{
    public partial class DBusConnection
    {
        #region Fields

        private AsyncReadyCallbackAsyncHandler? _callAsyncCallbackHandler;

        #endregion

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

            //TODO: Use on time CallbackHandler
            _callAsyncCallbackHandler = new AsyncReadyCallbackAsyncHandler(Callback);

            Internal.DBusConnection.Call(Handle, busName, objectPath, interfaceName, methodName, parameters.GetSafeHandle(), GLib.Internal.VariantTypeNullHandle.Instance, DBusCallFlags.None, -1, IntPtr.Zero, _callAsyncCallbackHandler.NativeCallback, IntPtr.Zero);

            return tcs.Task;
        }

        public Variant Call(string busName, string objectPath, string interfaceName, string methodName, Variant? parameters = null)
        {
            var parameterHandle = parameters?.Handle ?? GLib.Internal.VariantNullHandle.Instance;
            var ret = Internal.DBusConnection.CallSync(Handle, busName, objectPath, interfaceName, methodName, parameterHandle, GLib.Internal.VariantTypeNullHandle.Instance, DBusCallFlags.None, 9999, IntPtr.Zero, out var error);

            Error.ThrowOnError(error);

            return new Variant(ret);
        }

        public override void Dispose()
        {
            _callAsyncCallbackHandler?.Dispose();
            base.Dispose();
        }

        #endregion
    }
}
