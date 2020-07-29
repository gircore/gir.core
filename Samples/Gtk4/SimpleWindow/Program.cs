using System;
using Gtk.Core;

namespace Gtk4Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = new Program();
        }

        public Program()
        {
            var app = new GApplication("org.gircore.minimal");
            app.Activate += OnActivate;
            app.Run();
        }

        private void OnActivate(object? sender, EventArgs args)
        {
            if(sender is GApplication app)
            {
                var w = new GApplicationWindow(app);
                w.DefaultHeight.Value = 600;
                w.DefaultWidth.Value = 800;

                var box = new GBox();
                w.SetChild(box);
                
                var button = new GButton("Hello Gtk4");
                button.Clicked += (sender, args) => Console.WriteLine("Hello dear user");
                
                box.Append(button);
                w.Show();
            }
        }
    }
}
