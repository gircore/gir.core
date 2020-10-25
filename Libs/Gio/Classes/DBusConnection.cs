using System;
using System.Threading.Tasks;
using GLib;

namespace Gio
{
    public partial class DBusConnection
    {
        #region Methods
        public static DBusConnection Get(BusType busType)
        {
            var handle = Global.bus_get_sync(busType, IntPtr.Zero, out var error);
            HandleError(error);

            if (GetObject(handle, out DBusConnection obj))
                return obj;

            return new DBusConnection(handle);
        }

        public Task<Variant> CallAsync(string busName, string objectPath, string interfaceName, string methodName,
            Variant? parameters = null)
        {
            var tcs = new TaskCompletionSource<Variant>();

            void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
            {
                var ret = Native.call_finish(sourceObject, res, out var error);
                HandleError(error);

                tcs.SetResult(new Variant(ret));
            }

            var @params = parameters?.Handle ?? IntPtr.Zero;
            Native.call(Handle, busName, objectPath, interfaceName, methodName, @params, IntPtr.Zero, DBusCallFlags.None, -1, IntPtr.Zero, Callback, IntPtr.Zero);

            return tcs.Task;
        }

        #endregion
    }
}
