using System;
using System.IO;
using System.Reflection;
using System.Text;

// Important:
// Using Gtk-Builder is not as flexible as using composite templates
// please use composite templates instead (see sample) as those are
// fully featured and cleaner to use.

var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    BuilderSample.SampleTestDialog.GetGType(); //TODO: Gets obsolete with automatic subclass registration

    var builder = CreateBuilder("SampleTestDialog.4.ui");
    var dialog = (BuilderSample.SampleTestDialog) (builder.GetObject("dialog") ?? throw new Exception("Dialog is null")); ;
    dialog.Connect(builder);

    dialog.Application = (Gtk.Application) sender;
    dialog.Show();
    dialog.OnResponse += (_, _) => application.Quit();
};

return application.RunWithSynchronizationContext(null);

static Gtk.Builder CreateBuilder(string template)
{
    using var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(template);

    if (stream is null)
        throw new Exception($"Cannot get resource file '{template}'");

    using var ms = new MemoryStream();
    stream.CopyTo(ms);

    var buffer = ms.ToArray();
    var xml = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

    return Gtk.Builder.NewFromString(xml, -1);
}
