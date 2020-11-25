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
            IntPtr handle = Global.Native.bus_get_sync(busType, IntPtr.Zero, out IntPtr error);
            Error.HandleError(error);

            if (GetObject(handle, out DBusConnection obj))
                return obj;

            return new DBusConnection(handle);
        }
        
        #endregion
        
        #region Methods

        public Task<Variant> CallAsync(string busName, string objectPath, string interfaceName, string methodName,
            Variant? parameters = null)
        {
            var tcs = new TaskCompletionSource<Variant>();

            void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
            {
                IntPtr ret = Native.call_finish(sourceObject, res, out IntPtr error);
                Error.HandleError(error);

                tcs.SetResult(new Variant(ret));
            }

            IntPtr @params = parameters?.Handle ?? IntPtr.Zero;
            Native.call(Handle, busName, objectPath, interfaceName, methodName, @params, IntPtr.Zero, DBusCallFlags.None, -1, IntPtr.Zero, Callback, IntPtr.Zero);

            return tcs.Task;
        }

        public Variant Call(string busName, string objectPath, string interfaceName, string methodName, Variant? parameters = null)
        {
            IntPtr @params = parameters?.Handle ?? IntPtr.Zero;
            IntPtr ret = Native.call_sync(Handle, busName, objectPath, interfaceName, methodName, @params, IntPtr.Zero, DBusCallFlags.None, 9999, IntPtr.Zero, out IntPtr error);

            Error.HandleError(error);

            return new Variant(ret);
        }
        
        #endregion
    }
}
