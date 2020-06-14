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

        public Task<GVariant> CallAsync(string busName, string objectPath, string interfaceName, string methodName)
        {
            var tcs = new TaskCompletionSource<GVariant>();

            AsyncReadyCallback cb = (sourceObject, res, userData) =>
            {
                var ret = DBusConnection.call_finish(sourceObject, res, out var error);
                HandleError(error);

                tcs.SetResult(new GVariant(ret));
            };

            DBusConnection.call(this, busName, objectPath, interfaceName, methodName, IntPtr.Zero, IntPtr.Zero, DBusCallFlags.none, -1, IntPtr.Zero, cb, IntPtr.Zero);

            return tcs.Task;
        }

        public GVariant Call(string busName, string objectPath, string interfaceName, string methodName)
        {
            var ret = DBusConnection.call_sync(this, busName, objectPath, interfaceName, methodName, IntPtr.Zero, IntPtr.Zero, DBusCallFlags.none, -1, IntPtr.Zero, out var error);

            HandleError(error);

            return new GVariant(ret);
        }
    }
}