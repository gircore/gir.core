using System;
using Gio;
using GLib;

namespace Sample;

public static partial class DBus
{
    public static void ReadDesktopAppearanceColorScheme()
    {
        Console.WriteLine("Press enter to read desktop appearance color scheme...");
        Console.ReadLine();

        //Portal spec: https://docs.flatpak.org/en/latest/portal-api-reference.html#gdbus-org.freedesktop.portal.Settings

        var bus = DBusConnection.Get(BusType.Session);
        using var parameters = Variant.NewTuple(new[] {
            Variant.NewString("org.freedesktop.appearance"),
            Variant.NewString("color-scheme"),
        });

        using Variant ret = bus.CallSync(
            busName: "org.freedesktop.portal.Desktop",
            objectPath: "/org/freedesktop/portal/desktop",
            interfaceName: "org.freedesktop.portal.Settings",
            methodName: "Read",
            parameters: parameters,
            replyType: VariantType.New("(v)"),
            flags: DBusCallFlags.None,
            timeoutMsec: 9999,
            cancellable: null
        );
        var mode = ret.GetChildValue(0).GetVariant().GetVariant().GetUint32() switch
        {
            0 => "No Preference",
            1 => "DarkMode",
            2 => "LightMode",
            _ => "Unknown"
        };
        Console.WriteLine("Result: " + mode);
    }
}
