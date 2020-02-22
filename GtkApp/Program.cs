using System;
using Gtk.Core;

namespace GtkApp
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var app = new Program();
        }

        public Program()
        {
            var app = new GApplication("org.GtkApp");
            app.Activate += OnActivate;
            app.Startup += OnStartup;
            app.Run();
        }

        private void OnStartup(object? sender, EventArgs args)
        {
            if(sender is GApplication app)
            {
                var menu = new GMenu("menu.glade");
                app.SetAppMenu(menu);
            }
        }

        private void OnActivate(object? sender, EventArgs args)
        {
            if(sender is GApplication app)
            {
                var headerBar = new GHeaderBar();
                headerBar.Title.Value = "Title";
                headerBar.Subtitle.Value = "Subtitle";
                headerBar.ShowCloseButton.Value = true;

                var w = new MyWindow(app);
                w.DefaultHeight.Value = 800;
                w.DefaultWidth.Value = 400;
                w.SetTitlebar(headerBar);
                w.ShowAll();
            }
        }
    }
}
