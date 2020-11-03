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
            var app = new Application("org.gircore.minimal");
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

                w.PropertyChanged += (s, a) =>
                    Trace.WriteLine(
                        $"  => Property Changed On DemoWindow: {a.PropertyName} = {typeof(DemoWindow).GetProperty(a.PropertyName)?.GetValue(w)}");

                w.ShowAll();

                Trace.WriteLine("Getters using properties");
                Trace.Indent();
                Trace.WriteLine($"ApplicationId = {w.Application.ApplicationId}");
                Trace.WriteLine($"DefaultHeight = {w.DefaultHeight}");
                Trace.WriteLine($"DefaultWidth = {w.DefaultWidth}");
                Trace.Unindent();

                Trace.WriteLine("Getters using descriptors");
                Trace.Indent();
                Trace.WriteLine($"ApplicationProperty.Get() = {Window.ApplicationProperty.Get(w).ApplicationId}");
                Trace.WriteLine($"DefaultHeightProperty.Get() = {Window.DefaultHeightProperty.Get(w)}");
                Trace.WriteLine($"DefaultWidthProperty.Get() = {Window.DefaultWidthProperty.Get(w)}");
                Trace.Unindent();
            }
        }
    }
}
