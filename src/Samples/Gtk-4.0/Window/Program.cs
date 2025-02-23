var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var window = Gtk.ApplicationWindow.New((Gtk.Application) sender);
    window.Title = "Gtk4 Window";
    window.SetDefaultSize(300, 300);
    window.Show();
};
return application.RunWithSynchronizationContext(null);
