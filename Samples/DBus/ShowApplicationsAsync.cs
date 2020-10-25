using Gio;
using System;
using GLib;

namespace Sample
{
    public static partial class DBus
    {
        public static async void ShowApplicationsAsync()
        {
            Console.WriteLine("Press enter to show applications...");
            Console.ReadLine();
            
            var bus = DBusConnection.Get(BusType.session);
            using Variant ret = await bus.CallAsync(
                busName: "org.gnome.Panel", 
                objectPath: "/org/gnome/Shell", 
                interfaceName: "org.gnome.Shell", 
                methodName: "ShowApplications"
            );

            Console.WriteLine(ret.Print(true));
        }
    }
}
