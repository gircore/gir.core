using Gtk;

Gtk.Module.Initialize();

var application = Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var dialog = new AboutDialog.SampleAboutDialog("Custom AboutDialog");
    dialog.Application = (Application) sender;
    dialog.Show();
};

return application.Run();
