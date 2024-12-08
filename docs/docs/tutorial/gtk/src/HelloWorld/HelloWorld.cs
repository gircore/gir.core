namespace HelloWorld
{
    // Define a class named HelloWorld that extends Gtk.Window.
    // Gtk.Window represents a top-level window in a GTK application.
    public class HelloWorld : Gtk.Window
    {
        // Constructor for the HelloWorld class.
        // This method sets up the window and its contents.
        public HelloWorld()
        {
            // Set the title of the window.
            Title = "Hello World App";

            // Set the default size of the window to 300 pixels wide and 30 pixels tall.
            // This is the initial size when the window is first displayed.
            SetDefaultSize(300, 30);

            // Create a new button widget.
            // Gtk.Button is a clickable UI element.
            var helloButton = Gtk.Button.New();

            // Set the text label displayed on the button.
            helloButton.Label = "Say Hello";

            // Add margins around the button to create spacing from the edges of the window.
            // Margins are specified in pixels.
            helloButton.SetMarginStart(10);  // Left margin
            helloButton.SetMarginEnd(10);    // Right margin
            helloButton.SetMarginTop(10);    // Top margin
            helloButton.SetMarginBottom(10); // Bottom margin

            // Add the button to the window.
            // The `Child` property represents the main widget contained in the window.
            // Here, we set the button as the only child of the window.
            // In a real application you would set the child to be one of the Gtk Layout widgets 
            // so you can have multiple widgets in the window.
            Child = helloButton;

            // Attach an event handler to the button's "OnClicked" event.
            // This event is triggered when the user clicks the button.
            helloButton.OnClicked += (_, _) =>
            {
                // Print "Hello World!" to the console when the button is clicked.
                Console.WriteLine("Hello World!\n");
            };
        }
    }
}
