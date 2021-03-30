using System;
using System.Threading.Tasks;
using GLib;

namespace Gio
{
    public partial class DBusConnection
    {
        #region Static methods

        public static DBusConnection Get(BusType busType)
        {
            var handle = Functions.Native.BusGetSync(busType, IntPtr.Zero, out var error);
            Error.ThrowOnError(error);

            return Wrapper.WrapHandle<DBusConnection>(handle, true);
        }

        #endregion

        #region Methods

        /* TODO
        public Task<Variant> CallAsync(string busName, string objectPath, string interfaceName, string methodName,
            Variant? parameters = null)
        {
            var tcs = new TaskCompletionSource<Variant>();

            //TODO: This could be garbage collected and the callback would not work anymore
            void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
            {
                IntPtr ret = Native.call_finish(sourceObject, res, out IntPtr error);
                Error.ThrowOnError(error);

                tcs.SetResult(new Variant(ret));
            }

            IntPtr @params = parameters?.Handle ?? IntPtr.Zero;
            Native.call(Handle, busName, objectPath, interfaceName, methodName, @params, IntPtr.Zero, DBusCallFlags.None, -1, IntPtr.Zero, Callback, IntPtr.Zero);

            return tcs.Task;
        }*/

        public Variant Call(string busName, string objectPath, string interfaceName, string methodName, Variant? parameters = null)
        {
            var ret = Native.Instance.Methods.CallSync(Handle, busName, objectPath, interfaceName, methodName, parameters?.Handle, null, DBusCallFlags.None, 9999, IntPtr.Zero, out var error);

            Error.ThrowOnError(error);

            return new Variant(ret);
        }

        #endregion
    }
}
