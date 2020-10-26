using Gio;
using System;
using GLib;

namespace Sample
{
    public partial class DBus
    {
        public static void SendNotification()
        {
            /*TODO: FIX
            Console.WriteLine("Press enter to send notification...");
            Console.ReadLine();

            //Notification spec: https://developer.gnome.org/notification-spec/

            var bus = Connection.Get(BusType.Session);
            using var parameters = new Variant(
                new Variant("AppName"),
                new Variant(0u),
                new Variant(""), //Icon
                new Variant("Summary"),
                new Variant("Body"),
                new Variant(new string[0]),
                Variant.CreateEmptyDictionary(VariantType.String, VariantType.Variant),//hints
                new Variant(999)
            );

            using var ret2 = bus.Call("org.freedesktop.Notifications", "/org/freedesktop/Notifications", "org.freedesktop.Notifications", "Notify", parameters);
            */
        }
    }
}