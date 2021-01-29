using System;
using Gio;
using GLib;

namespace Sample
{
    public static partial class DBus
    {
        public static void SendNotification()
        {
            Console.WriteLine("Press enter to send notification...");
            Console.ReadLine();

            //Notification spec: https://developer.gnome.org/notification-spec/

            var bus = DBusConnection.Get(BusType.Session);
            using var parameters = new Variant(
                Variant.Create("AppName"),
                Variant.Create(0u),
                Variant.Create(""), //Icon
                Variant.Create("Summary"),
                Variant.Create("Body"),
                Variant.Create(new string[0]),
                Variant.CreateEmptyDictionary(VariantType.String, VariantType.Variant),//hints
                Variant.Create(999)
            );

            using Variant ret = bus.Call("org.freedesktop.Notifications", "/org/freedesktop/Notifications", "org.freedesktop.Notifications", "Notify", parameters);
        }
    }
}
