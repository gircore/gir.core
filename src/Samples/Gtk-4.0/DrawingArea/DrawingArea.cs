using Gtk;

Gtk.Module.Initialize();

var application = Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var drawingArea = DrawingArea.New();
    drawingArea.SetDrawFunc(Draw);

    var window = ApplicationWindow.New((Application) sender);
    window.Title = "DrawingArea Demo";
    window.Child = drawingArea;
    window.Show();
};

void Draw(Gtk.DrawingArea drawingArea, Cairo.Context cr, int width, int height)
{
    cr.SetSourceRgba(0.1, 0.1, 0.1, 1.0);
    cr.MoveTo(20, 30);
    cr.ShowText("This is some text, drawn with cairo");

    cr.MoveTo(40, 60);
    cr.ShowText("Powered by gir.core - GObject bindings for .NET");
}
return application.Run();
