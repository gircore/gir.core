using GObject;
using Gtk;
using Global = Gtk.Global;

namespace GtkDemo
{
    public class Program
    {
        private static Window _window1 = null!;
        private static Window _window2 = null!;

        public static void Main(string[] args)
        {
            Global.Init();

            // Awesome windows initializations <3, almost the MVU style ^^

            _window1 = new Window("HeloWorld")
            {
                Child = new Button("Open")
                {
                    [Button.ClickedSignal] = OnOpenButtonClick,
                },
            };

            _window1.ShowAll();

            Global.Main();

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
