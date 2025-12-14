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
        using var parameters = Variant.NewTuple(new[] {
            Variant.NewString("AppName"),
            Variant.NewUint32(0u),
            Variant.NewString(""), //Icon
            Variant.NewString("Summary"),
            Variant.NewString("Body"),
            Variant.NewStrv(Array.Empty<string>()),
            Variant.NewArray(VariantType.NewDictEntry(VariantType.New("s"), VariantType.New("v")), null), //hints
            Variant.NewInt32(999)
        });

        using Variant ret = bus.CallSync("org.freedesktop.Notifications", "/org/freedesktop/Notifications", "org.freedesktop.Notifications", "Notify", parameters, null, DBusCallFlags.None, 9999, null);
        Console.WriteLine("Result: " + ret.Print(true));
    }
}
