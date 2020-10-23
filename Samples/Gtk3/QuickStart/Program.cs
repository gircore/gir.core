using GObject;
using Gtk;
using Global = Gtk.Global;

namespace GtkDemo
{
    /// <summary>
    /// Minimalist demo program demonstrating the core
    /// features of Gir.Core
    /// </summary>
    public class Program
    {
        // Our Gtk Widgets
        private static Window mainWindow = null!;
        private static Window? popupWindow;

        // Entry Point
        public static void Main(string[] args)
        {
            // We need to call Gtk.Global.Init() before using
            // any Gtk widgets or functions. If you use Gtk.Application,
            // this is done for you.
            Global.Init();

            // Gir.Core supports Object Initialiser Syntax for every widget,
            // allowing for entire widget trees to be created using a nice,
            // almost MVU-style syntax:
            mainWindow = new Window("Hello World!")
            {
                // Set the default size of our window
                DefaultWidth = 800,
                DefaultHeight = 600,

                // Set the child property of mainWindow to
                // a Gtk.Notebook widget. This creates tabs which
                // the user can use to switch between different 'Pages'.
                Child = new Notebook()
                {
                    // Register a callback for switching pages
                    [Notebook.SwitchPageSignal] = OnPageSwitched,

                    // Add some widgets to the notebook
                    ["Page1"] = new Label("Hello C#"),
                    ["Page2"] = new Button("Open")
                    {
                        // Register a callback for the button
                        [Button.ClickedSignal] = OnOpenButtonClick,
                    }
                },

                // Setup our application to quit when the main
                // window is closed. We can use delegates as well as
                // ordinary methods for signal callbacks.
                [Window.DestroySignal] = (o, e) => Global.MainQuit()
            };

            // Show our window. In Gtk3, widgets are hidden by default.
            // We need to tell Gtk that our window should be visible
            // to the user.
            mainWindow.ShowAll();

            // Call Gtk.Global.Main() to start our application
            // main loop. The program will keep on running until
            // Gtk.Global.MainQuit() is called.
            Global.Main();

            // Finally, clean up after ourselves and dispose of the
            // window widget. This is not required, but it is good
            // practice to dispose of widgets explicitly.
            mainWindow?.Dispose();
        }

        /// <summary>
        /// This method is called whenever the user switches pages
        /// in the notebook. We print out the page number and its contents.
        /// </summary>
        public static void OnPageSwitched(object? sender, Notebook.SwitchPageSignalArgs args)
        {
            System.Console.WriteLine($"SwitchedPage: {args.page_num} with child {args.page.GetType().Name}");
        }

        /// <summary>
        /// This method is called when the user clicks on our button
        /// widget. We create a new window and display it to the user
        /// with some text.
        /// </summary>
        public static void OnOpenButtonClick(object? sender, SignalArgs args)
        {
            // If we already have a popup window, show that and return.
            if (popupWindow is {})
            {
                popupWindow.ShowAll();
                return;
            }

            // Otherwise, create a new Gtk.Window widget
            popupWindow = new Gtk.Window("Another Window")
            {
                // Set our default size
                DefaultWidth = 400,
                DefaultHeight = 300,

                // Add a button to this Window
                Child = new Button("Close")
                {
                    // When the button is clicked, make sure the
                    // 'OnCloseButtonClick' method is called.
                    [Button.ClickedSignal] = OnCloseButtonClick,
                }
            };

            // Finally, show our popup window
            popupWindow.ShowAll();
        }

        /// <summary>
        /// This method is called whenever the button inside our Popup
        /// Window is clicked. We simply instruct Gtk to close our window.
        /// </summary>
        public static void OnCloseButtonClick(object? sender, SignalArgs args)
        {
            // Close the window
            popupWindow!.Close();

            // Unset popupWindow
            popupWindow.Dispose();
            popupWindow = null;
        }
    }
}