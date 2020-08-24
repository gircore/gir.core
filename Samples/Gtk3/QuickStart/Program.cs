using System;
using GObject;
using Gtk;

namespace GtkDemo
{
    public class Program
    {
        private static Window _window1 = null!;
        private static Window _window2 = null!;

        public static void Main(string[] args)
        {
            var argc = 0;
            var argv = IntPtr.Zero;

            Gtk.Sys.Methods.init(ref argc, ref argv);

            // Awesome windows initializations <3, almost the MVU style ^^

            _window1 = new Gtk.Window("Hello World")
            {
                Child = new Button("Open")
                {
                    [Button.ClickedSignal] = OnOpenButtonClick,
                },
            };

            _window1.ShowAll();

            Gtk.Sys.Methods.main();

            _window1?.Dispose();
            _window2?.Dispose();
        }

        public static void OnOpenButtonClick(object? sender, SignalArgs args)
        {
            _window2 = new Gtk.Window("Another Window")
            {
                DefaultWidth = 400,
                DefaultHeight = 300,
                Child = new Button("Close")
                {
                    [Button.ClickedSignal] = OnCloseButtonClick,
                }
            };

            _window2.ShowAll();
        }

        public static void OnCloseButtonClick(object? sender, SignalArgs args)
        {
            _window2?.Close();
        }
    }
}
