using System;
using System.Threading.Tasks;
using GLib;
using GObject;

namespace Gio.DBus
{
    public partial class Connection : GObject.Object
    {
        #region Properties
        public Property<string> Address { get; }
        public ReadOnlyProperty<bool> Closed { get; }
        #endregion Properties

        public Connection(string address) : this(Sys.DBusConnection.new_for_address_sync(address, Sys.DBusConnectionFlags.none, IntPtr.Zero, IntPtr.Zero, out var error))
        {
            HandleError(error);
        }

        internal Connection(IntPtr handle) : base(handle)
        {
            Address = PropertyOfString("address");
            Closed = ReadOnlyPropertyOfBool("closed");
        }

        public Variant Call(string busName, string objectPath, string interfaceName, string methodName, Variant? parameters = null)
        {
            var @params = parameters?.Handle ?? IntPtr.Zero;

            var ret = Sys.DBusConnection.call_sync(this, busName, objectPath, interfaceName, methodName, @params, IntPtr.Zero, Sys.DBusCallFlags.none, 9999, IntPtr.Zero, out var error);

            HandleError(error);

            return new Variant(ret);
        }


        public Task<Variant> CallAsync(string busName, string objectPath, string interfaceName, string methodName, Variant? parameters = null)
        {
            var tcs = new TaskCompletionSource<Variant>();

            void Callback(IntPtr sourceObject, IntPtr res, IntPtr userData)
            {
                var ret = Sys.DBusConnection.call_finish(sourceObject, res, out var error);
                HandleError(error);

                tcs.SetResult(new Variant(ret));
            }

            var @params = parameters?.Handle ?? IntPtr.Zero;
            Sys.DBusConnection.call(this, busName, objectPath, interfaceName, methodName, @params, IntPtr.Zero, Sys.DBusCallFlags.none, -1, IntPtr.Zero, Callback, IntPtr.Zero);

            return tcs.Task;
        }
    }
}