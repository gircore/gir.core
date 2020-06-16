using System;
using Gir.Core.Gst;
using Gtk.Core;
using GtkClutter.Core;

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
            /*var app = new GApplication("org.GtkApp");
            app.InitClutter();
            app.Activate += OnActivate;
            app.Startup += OnStartup;
            app.Run();*/

            Gir.Core.Gst.Application.Init();
            var ret = Parse.Launch("playbin uri=playbin uri=http://download.blender.org/durian/trailer/sintel_trailer-1080p.mp4");
            ret.SetState(State.Playing);
            var bus = ret.GetBus();
            bus.TimedPopFiltered(18446744073709551615);
            ret.SetState(State.Null);
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
