var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var mainWindow = new Gtk.Window
    {
        Application = (Gtk.Application) sender,
        DefaultWidth = 800,
        DefaultHeight = 600,
        Title = "Hello declarative ui!",

        Child = new Gtk.Button
        {
            Label = "Close applicatin",
            [Gtk.Button.ClickedSignal] = (s, a) => ((Gtk.Application) sender).Quit()
        },
    };
    mainWindow.Show();
};
return application.Run();
