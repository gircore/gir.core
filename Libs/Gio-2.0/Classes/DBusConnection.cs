using System;
using System.Threading.Tasks;
using GLib;

namespace Gio
{
    public partial class DBusConnection
    {
        #region Fields

        private AsyncReadyCallbackNativeCallHandler? _callAsyncCallbackHandler;
        
        #endregion
        
        #region Static methods

        public static DBusConnection Get(BusType busType)
        {
            var handle = Functions.Native.BusGetSync(busType, IntPtr.Zero, out var error);
            Error.ThrowOnError(error);

            return Wrapper.WrapHandle<DBusConnection>(handle, true);
        }

        #endregion

        #region Methods

        public Task<Variant> CallAsync(string busName, string objectPath, string interfaceName, string methodName,
            Variant? parameters = null)
        {
            var tcs = new TaskCompletionSource<Variant>();

            void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
            {
                var ret = Native.Instance.Methods.CallFinish(sourceObject, res, out var error);
                Error.ThrowOnError(error);

                tcs.SetResult(new Variant(ret));
            }
            
            //TODO: Use on time CallbackHandler
            _callAsyncCallbackHandler = new AsyncReadyCallbackNativeCallHandler(Callback);

            Native.Instance.Methods.Call(Handle, busName, objectPath, interfaceName, methodName, parameters.GetSafeHandle(), VariantType.Native.VariantTypeSafeHandle.Null, DBusCallFlags.None, -1, IntPtr.Zero, _callAsyncCallbackHandler.NativeCallback, IntPtr.Zero);

            return tcs.Task;
        }

        public Variant Call(string busName, string objectPath, string interfaceName, string methodName, Variant? parameters = null)
        {
            var ret = Native.Instance.Methods.CallSync(Handle, busName, objectPath, interfaceName, methodName, parameters?.Handle, VariantType.Native.VariantTypeSafeHandle.Null, DBusCallFlags.None, 9999, IntPtr.Zero, out var error);

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
