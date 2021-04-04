using System;
using System.Threading.Tasks;
using GLib;

namespace Gio
{
    public partial class DBusConnection
    {
        #region Fields

        private Native.AsyncReadyCallbackNativeCallHandler? _callAsyncCallbackHandler;
        
        #endregion
        
        #region Static methods

        public static DBusConnection Get(BusType busType)
        {
            var handle = Native.Functions.BusGetSync(busType, IntPtr.Zero, out var error);
            Error.ThrowOnError(error);

            return GObject.Native.ObjectWrapper.WrapHandle<DBusConnection>(handle, true);
        }

        #endregion

        #region Methods

        public Task<Variant> CallAsync(string busName, string objectPath, string interfaceName, string methodName,
            Variant? parameters = null)
        {
            var tcs = new TaskCompletionSource<Variant>();

            void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
            {
                var ret = Native.DBusConnection.Instance.Methods.CallFinish(sourceObject, res, out var error);
                Error.ThrowOnError(error);

                tcs.SetResult(new Variant(ret));
            }
            
            //TODO: Use on time CallbackHandler
            _callAsyncCallbackHandler = new Native.AsyncReadyCallbackNativeCallHandler(Callback);

            Native.DBusConnection.Instance.Methods.Call(Handle, busName, objectPath, interfaceName, methodName, parameters.GetSafeHandle(), GLib.Native.VariantType.Handle.Null, DBusCallFlags.None, -1, IntPtr.Zero, _callAsyncCallbackHandler.NativeCallback, IntPtr.Zero);

            return tcs.Task;
        }

        public Variant Call(string busName, string objectPath, string interfaceName, string methodName, Variant? parameters = null)
        {
            var ret = Native.DBusConnection.Instance.Methods.CallSync(Handle, busName, objectPath, interfaceName, methodName, parameters?.Handle, GLib.Native.VariantType.Handle.Null, DBusCallFlags.None, 9999, IntPtr.Zero, out var error);

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
