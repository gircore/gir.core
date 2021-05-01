using System;
using System.Diagnostics;
using Gtk;
using Application = Gtk.Application;

namespace GtkDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = new Program();
        }

        public Program()
        {
            var app = new Application("org.gircore.builder");
            app.OnActivate += OnActivate;
            app.Run();
        }

        private void OnActivate(Gio.Application sender, EventArgs args)
        {
            if (sender is Application app)
            {
                var w = new DemoWindow(app)
                {
                    DefaultHeight = 600,
                    DefaultWidth = 800,
                };

                w.ShowAll();
            }
        }
    }
}
