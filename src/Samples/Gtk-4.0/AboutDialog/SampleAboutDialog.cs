using System;
using System.Reflection;
using GdkPixbuf;
using GLib;
using GObject;

namespace AboutDialog;

[Subclass<Gtk.AboutDialog>]
public partial class SampleAboutDialog
{
    public static SampleAboutDialog New(string sampleName)
    {
        var obj = NewWithProperties([]);
        obj.Authors = ["Gir.Core Developers", "badcel", "mjakeman"];
        obj.Comments = "Gir.Core is a C# wrapper for GObject based libraries providing a C# friendly API surface.";
        obj.Copyright = "© Gir.Core Developers 2021-present";
        obj.License = "MIT License";
        obj.Logo = LoadFromResource("AboutDialog.logo.svg");
        obj.Version = "0.1.0";
        obj.Website = "https://gircore.github.io/";
        obj.LicenseType = Gtk.License.MitX11;
        obj.ProgramName = $"{sampleName} - GirCore";

        return obj;
    }

    private static Gdk.Texture LoadFromResource(string resourceName)
    {
        try
        {
            var data = Assembly.GetExecutingAssembly().ReadResourceAsByteArray(resourceName);
            using var bytes = Bytes.New(data);
            var pixbufLoader = PixbufLoader.New();
            pixbufLoader.WriteBytes(bytes);
            pixbufLoader.Close();

            var pixbuf = pixbufLoader.GetPixbuf() ?? throw new Exception("No pixbuf loaded");
            return Gdk.Texture.NewForPixbuf(pixbuf);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unable to load image resource '{resourceName}': {e.Message}");
            return null;
        }
    }
}
