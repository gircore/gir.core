using System.Reflection;
using CompositeTemplate;

Gtk.Module.Initialize();
GirCore.Integration.Initialize();

var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var window = Gtk.ApplicationWindow.New((Gtk.Application) sender);
    window.Title = "Gtk4 Window";
    window.SetDefaultSize(300, 300);
    window.Child = CompositeBoxWidget.NewWithProperties([]);
    window.Show();
};

using var stream = Assembly.GetExecutingAssembly()
    .GetManifestResourceStream("CompositeTemplate.app.gresource");

var buffer = new byte[stream!.Length];
stream.ReadExactly(buffer);

using var bytes = GLib.Bytes.New(buffer);
using var resource = Gio.Resource.NewFromData(bytes);
resource.Register();

return application.RunWithSynchronizationContext(null);
