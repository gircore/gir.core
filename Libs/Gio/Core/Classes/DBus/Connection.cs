using System;
using System.Threading.Tasks;
using GLib.Core;
using GObject.Core;

namespace Gio.Core.DBus
{
    public partial class Connection : GObject.Core.GObject
    {
        #region Properties
        public Property<string> Address { get; }
        public ReadOnlyProperty<bool> Closed { get; }
        #endregion Properties

        public Connection(string address) : this(Gio.DBusConnection.new_for_address_sync(address, DBusConnectionFlags.none, IntPtr.Zero, IntPtr.Zero, out var error))
        {
            HandleError(error);
        }

        internal Connection(IntPtr handle) : base(handle)
        {
            Address = PropertyOfString("address");
            Closed = ReadOnlyPropertyOfBool("closed");
        }

        public GVariant Call(string busName, string objectPath, string interfaceName, string methodName, GVariant? parameters = null)
        {
            var @params = parameters?.Handle ?? IntPtr.Zero;

            var ret = DBusConnection.call_sync(this, busName, objectPath, interfaceName, methodName, @params, IntPtr.Zero, DBusCallFlags.none, 9999, IntPtr.Zero, out var error);

            HandleError(error);

            return new GVariant(ret);
        }


        public Task<GVariant> CallAsync(string busName, string objectPath, string interfaceName, string methodName, GVariant? parameters = null)
        {
            var tcs = new TaskCompletionSource<GVariant>();

            AsyncReadyCallback cb = (sourceObject, res, userData) =>
            {
                var ret = DBusConnection.call_finish(sourceObject, res, out var error);
                HandleError(error);

                tcs.SetResult(new GVariant(ret));
            };

            var @params = parameters?.Handle ?? IntPtr.Zero;
            DBusConnection.call(this, busName, objectPath, interfaceName, methodName, @params, IntPtr.Zero, DBusCallFlags.none, -1, IntPtr.Zero, cb, IntPtr.Zero);

            return tcs.Task;
        }
    }
}