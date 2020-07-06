using Gio.Core.DBus;
using GLib.Core;
using System;
using System.Collections.Generic;

namespace Sample
{
    public class DBus
    {
        public static async void ExecuteDBusCall()
        {
            Console.WriteLine("Press enter to execute DBus call...");
           // Console.ReadLine();
            
            var t =  new GVariant(new string[0]);

            var c = Connection.Get(BusType.Session);

            //using var ret = await c.CallAsync("org.gnome.Panel", "/org/gnome/Shell", "org.gnome.Shell", "ShowApplications");
            //Console.WriteLine(ret.Print(true));

            var hints = new Dictionary<string, GVariant>();

            var parameters = new GVariant(
                new GVariant("AppName"),
                new GVariant(0u),
                new GVariant(""), //Icon
                new GVariant("Summary"),
                new GVariant("Body"),
                new GVariant(new string[0]),
                hints,//TODO
                new GVariant(999)
            );
            var ret2 = c.Call("org.freedesktop.Notifications", "/org/freedesktop/Notifications", "org.freedesktop.Notifications", "Notify", parameters);

            Console.ReadLine();
        }
    }
}