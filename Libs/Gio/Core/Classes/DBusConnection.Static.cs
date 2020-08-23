using System;
using Object = GObject.Object;

namespace Gio
{
    public partial class DBusConnection
    {
        public static DBusConnection Get(BusType busType)
        {
            var handle = Sys.Methods.bus_get_sync((Sys.BusType)busType, IntPtr.Zero, out var error);
            HandleError(error);
            return WrapPointerAs<DBusConnection>(handle);
        }
    }
}