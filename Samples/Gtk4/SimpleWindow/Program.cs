using System;
using Gtk;

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
            var app = new Application("org.gircore.minimal");
            app.Activate += OnActivate;
            app.Run();
        }

        private void OnActivate(object? sender, EventArgs args)
        {
            if(sender is Application app)
            {
                var w = new ApplicationWindow(app);
                w.DefaultHeight.Value = 600;
                w.DefaultWidth.Value = 800;

                var box = new Box();
                w.SetChild(box);
                
                var button = new Button("Hello Gtk4");
                button.Clicked += (sender, args) => Console.WriteLine("Hello dear user");
                
                box.Append(button);
                w.Show();
            }
        }
    }
}
