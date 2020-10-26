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
            IntPtr handle = Global.bus_get_sync(busType, IntPtr.Zero, out IntPtr error);
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
                IntPtr ret = Native.call_finish(sourceObject, res, out IntPtr error);
                HandleError(error);

                tcs.SetResult(new Variant(ret));
            }

            IntPtr @params = parameters?.Handle ?? IntPtr.Zero;
            Native.call(Handle, busName, objectPath, interfaceName, methodName, @params, IntPtr.Zero, DBusCallFlags.none, -1, IntPtr.Zero, Callback, IntPtr.Zero);

            return tcs.Task;
        }

        #endregion
    }
}