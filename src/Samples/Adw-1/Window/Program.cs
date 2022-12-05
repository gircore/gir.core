var application = Adw.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var label = Gtk.Label.New("Hello world");
    var window = Gtk.ApplicationWindow.New((Adw.Application) sender);
    window.Title = "Window";
    window.SetDefaultSize(300, 300);
    window.SetChild(label);
    window.Show();
};
return application.Run();
