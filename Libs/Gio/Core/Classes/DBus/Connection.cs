using System;
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
    }
}