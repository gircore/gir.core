var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var dialog = AboutDialog.SampleAboutDialog.New("Custom AboutDialog");
    dialog.Application = (Gtk.Application) sender;
    dialog.Show();
};

return application.RunWithSynchronizationContext(null);
