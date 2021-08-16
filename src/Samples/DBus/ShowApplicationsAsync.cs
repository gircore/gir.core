using System;
using Gio;
using GLib;

namespace Sample
{
    public static partial class DBus
    {
        public static async void ShowApplicationsAsync()
        {
            Console.WriteLine("Press enter to show applications...");
            Console.ReadLine();

            //var busname = "org.gnome.Panel"; //For gnome < 40
            var busname = "org.gnome.Shell"; //For gnome 40

            var bus = DBusConnection.Get(BusType.Session);
            using Variant ret = await bus.CallAsync(
                busName: busname,
                objectPath: "/org/gnome/Shell",
                interfaceName: "org.gnome.Shell",
                methodName: "ShowApplications"
            );

            Console.WriteLine(ret.Print(true));
        }
    }
}
