using System;
using GObject;
using Gtk;

namespace TextEditor
{
    using Application;

    public class App : Gtk.Application
    {
        private const string AppName = "org.gircore.TextEditor";

        public App()
        {
            this.ApplicationId = AppName;
            this.OnActivate += Activate;
        }

        private void Activate(object app, EventArgs args)
        {
            var window = new AppWindow();
            window.Application = this;
            window.Present();
        }

        public static int Main(string[] args)
        {
            Gtk.Module.Initialize();
            return new App().Run();
        }
    }
}
