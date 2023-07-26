using System;
using Gio;
using GLib;
using Array = System.Array;

namespace Sample;

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
            Variant.Create(Array.Empty<string>()),
            Variant.CreateEmptyDictionary(VariantType.String, VariantType.Variant),//hints
            Variant.Create(999)
        );

        using Variant ret = bus.CallSync("org.freedesktop.Notifications", "/org/freedesktop/Notifications", "org.freedesktop.Notifications", "Notify", parameters, null, DBusCallFlags.None, 9999, null);
        Console.WriteLine("Result: " + ret.Print(true));
    }
}
