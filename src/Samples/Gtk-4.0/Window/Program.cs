using Gtk;

Gtk.Module.Initialize();

var application = Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    //TODO: FOR TESTING ONLY
    
    var window = new MessageDialog();
    window.Text = "Helo";
    window.SecondaryText = "World";
    window.Application = (Application) sender;

    //var window = ApplicationWindow.New((Application) sender);
    window.Title = "Gtk4 Window";
    window.SetDefaultSize(300, 300);
    window.Show();
};
return application.Run();
