using System;
using Gtk;

namespace GtkDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var argc = 0;
            var argv = IntPtr.Zero;

            Gtk.Sys.Methods.init(ref argc, ref argv);

            var window = new Gtk.Window("Hello World");
            var button = new Gtk.Button("Text");
            window.Add(button);
            window.ShowAll();

            var window2 = new Gtk.Window("Goodbye?");
            window2.Show();
            
            Gtk.Sys.Methods.main();
        }
    }
}
