using System;

namespace Gio.DBus
{
    public partial class Connection
    {
        public static Connection Get(BusType busType)
        {
            var handle = Sys.Methods.bus_get_sync((Sys.BusType)busType, IntPtr.Zero, out var error);
            HandleError(error);

            return TryGetObject(handle, out Connection obj) ? obj : new Connection(handle);
        }
    }
}