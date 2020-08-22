using System;
using System.Diagnostics;
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
            if (sender is Application app)
            {
                var t = typeof(DemoWindow);

                var w = new DemoWindow(app)
                {
                    DefaultHeightEx = 600,
                    DefaultWidthEx = 800,
                };

                w.PropertyChanged += (s, a) => Trace.WriteLine($"  => Property Changed On DemoWindow: {a.PropertyName} = {t.GetProperty(a.PropertyName)?.GetValue(w)}");

                w.ShowAll();

                Trace.WriteLine("Getters using properties");
                Trace.Indent();
                Trace.WriteLine($"ApplicationId = {w.ApplicationEx.ApplicationId.Value}");
                Trace.WriteLine($"DefaultHeight = {w.DefaultHeightEx}");
                Trace.WriteLine($"DefaultWidth = {w.DefaultWidthEx}");
                Trace.Unindent();

                Trace.WriteLine("Getters using descriptors");
                Trace.Indent();
                Trace.WriteLine($"ApplicationProperty.Get() = {Window.ApplicationProperty.Get(w).ApplicationId.Value}");
                Trace.WriteLine($"DefaultHeightProperty.Get() = {Window.DefaultHeightProperty.Get(w)}");
                Trace.WriteLine($"DefaultWidthProperty.Get() = {Window.DefaultWidthProperty.Get(w)}");
                Trace.Unindent();
            }
        }
    }
}
