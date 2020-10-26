using Gio;
using System;

namespace Sample
{
    public partial class DBus
    {
        public static async void ShowApplicationsAsync()
        {
            Console.WriteLine("Press enter to show applications...");
            Console.ReadLine();
            
            var bus = DBusConnection.Get(BusType.session);
            using var ret = await bus.CallAsync("org.gnome.Panel", "/org/gnome/Shell", "org.gnome.Shell", "ShowApplications");

            Console.WriteLine(ret.Print(true));
        }
    }
}