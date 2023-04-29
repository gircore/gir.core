GtkSource.Module.Initialize();
var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var window = Gtk.ApplicationWindow.New((Gtk.Application) sender);
    window.Title = "GtkSource5 Demo";
    window.SetDefaultSize(300, 300);
    var buf = GtkSource.Buffer.New(null);
    var view = GtkSource.View.NewWithBuffer(buf);
    view.Monospace = true;
    view.ShowLineNumbers = true;
    var settings = Gtk.Settings.GetDefault();
    if (settings?.GtkApplicationPreferDarkTheme == true ||
        settings?.GtkThemeName?.ToLower()?.Contains("dark") == true)
        buf.SetStyleScheme(GtkSource.StyleSchemeManager.GetDefault().GetScheme("Adwaita-dark"));
    window.Child = view;
    window.Show();
};
return application.Run();
