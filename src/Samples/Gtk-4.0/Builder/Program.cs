var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var dialog = new BuilderSample.SampleTestDialog();
    dialog.Application = (Gtk.Application) sender;
    dialog.Show();
    dialog.OnResponse += (_, _) => application.Quit();
};

return application.RunWithSynchronizationContext(null);
