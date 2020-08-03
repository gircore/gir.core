using System;
using Gtk;

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
            var app = new Application("org.gircore.minimal");
            app.Activate += OnActivate;
            app.Run();
        }

        private void OnActivate(object? sender, EventArgs args)
        {
            if(sender is Application app)
            {
                var w = new DemoWindow(app);
                w.DefaultHeight.Value = 600;
                w.DefaultWidth.Value = 800;
                w.ShowAll();
            }
        }
    }
}
