using Gio.Core.DBus;
using GLib.Core;
using System;

namespace Sample
{
    public partial class DBus
    {
        public static void SendNotification()
        {
            Console.WriteLine("Press enter to send notification...");
            //Console.ReadLine();

            //Notification spec: https://developer.gnome.org/notification-spec/

            var bus = Connection.Get(BusType.Session);
            using var parameters = new GVariant(
                new GVariant("AppName"),
                new GVariant(0u),
                new GVariant(""), //Icon
                new GVariant("Summary"),
                new GVariant("Body"),
                new GVariant(new string[0]),
                GVariant.CreateEmptyDictionary(GVariantType.String, GVariantType.Variant),//hints
                new GVariant(999)
            );
            using var ret2 = bus.Call("org.freedesktop.Notifications", "/org/freedesktop/Notifications", "org.freedesktop.Notifications", "Notify", parameters);
        }
    }
}