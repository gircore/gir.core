using Gtk;

namespace DeclarativeUi
{
    /// <summary>
    /// Sample to demonstrate declaraitve ui features.
    /// </summary>
    public static class Program
    {
        public static void Main(string[] args)
        {
            // We need to call Gtk.Functions.Init() before using
            // any Gtk widgets or functions. If you use Gtk.Application,
            // this is done for you.
            Functions.Init();

            // Gir.Core supports Object Initialiser Syntax for every widget,
            // allowing for entire widget trees to be created:
            var mainWindow = new Window("Hello declarative ui!")
            {
                DefaultWidth = 800,
                DefaultHeight = 600,
                
                // Conenct the MainQuit function to the destroy signal
                // of the window to close the application if the window is closed
                [Window.DestroySignal] = (widget, args) => Functions.MainQuit(),
                
                Child = new Button("Close application")
                {
                    // Connect the MainQuit function to the clicked signal
                    // of the button to close the running application.
                    [Button.ClickedSignal] = (button, args) => Functions.MainQuit()
                },
            };

            // Show our window. In Gtk3, widgets are hidden by default.
            // We need to tell Gtk that our window should be visible
            // to the user.
            mainWindow.ShowAll();

            // Call Gtk.Global.Main() to start our application
            // main loop. The program will keep on running until
            // Gtk.Global.MainQuit() is called.
            Functions.Main();
        }
    }
}
