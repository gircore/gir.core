// Create a new GTK application instance.
// "com.example.helloworld" is the unique application ID used to identify the app.
// The application ID should be a domain name you control. 
// If you don't own a domain name you can use a project specific domain such as github pages. 
// e.g. io.github.projectname
// Gio.ApplicationFlags.FlagsNone indicates no special flags are being used.
var application = Gtk.Application.New("com.example.helloworld", Gio.ApplicationFlags.FlagsNone);

// Attach an event handler to the application's "OnActivate" event.
// This event is triggered when the application is started or activated.
application.OnActivate += (sender, args) =>
{
    // Create a new instance of the main application window.
    var window = new HelloWorld.HelloWorld();
    
    // Set the "Application" property of the window to the current application instance.
    // This links the window to the application, allowing them to work together.
    window.Application = (Gtk.Application) sender;
    
    // Show the window on the screen.
    // This makes the window visible to the user.
    window.Show();
};

// Start the application's event loop and process user interactions.
// RunWithSynchronizationContext ensures proper thread synchronization for GTK.
// The "null" parameter takes the arguments from the commandline. As there are no arguments
// supported in this tutorial the parameter is not filled and thus null.
return application.RunWithSynchronizationContext(null);
