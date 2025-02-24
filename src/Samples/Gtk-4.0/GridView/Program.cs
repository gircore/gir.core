var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var buttonStringListGridView = CreateButton("String List GridView Window");
    buttonStringListGridView.OnClicked += (_, _) => new GridViewSample.StringListGridViewWindow().Show();

    var buttonCustomObjectGridView = CreateButton("Custom Object GridView Window");
    buttonCustomObjectGridView.OnClicked += (_, _) => new GridViewSample.CustomObjectGridViewWindow().Show();

    var gtkBox = Gtk.Box.New(Gtk.Orientation.Vertical, 0);
    gtkBox.Append(buttonStringListGridView);
    gtkBox.Append(buttonCustomObjectGridView);

    var window = Gtk.ApplicationWindow.New((Gtk.Application) sender);
    window.Title = "GridView Sample";
    window.SetDefaultSize(300, 300);
    window.Child = gtkBox;
    window.Show();
};
return application.RunWithSynchronizationContext(null);

static Gtk.Button CreateButton(string label)
{
    var button = Gtk.Button.New();
    button.Label = label;
    button.SetMarginTop(12);
    button.SetMarginBottom(12);
    button.SetMarginStart(12);
    button.SetMarginEnd(12);
    return button;
}
