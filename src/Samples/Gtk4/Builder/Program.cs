using Gtk;

Gtk.Module.Initialize();

var application = Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var dialog = new BuilderSample.SampleTestDialog();
    dialog.Application = (Application) sender;
    dialog.Show();
    dialog.OnResponse += (_, _) => application.Quit();
};

return application.Run();
