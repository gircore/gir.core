using Gio.Core.DBus;
using System;

namespace Sample
{
    public class DBus
    {
        public static async void ExecuteDBusCall()
        {
            Console.WriteLine("Press enter to execute DBus call...");
            Console.ReadLine();
            
            var c = Connection.Get(BusType.Session);

            using var ret = await c.CallAsync("org.gnome.Panel", "/org/gnome/Shell", "org.gnome.Shell", "ShowApplications");
            Console.WriteLine(ret.Print(true));
        }
    }
}