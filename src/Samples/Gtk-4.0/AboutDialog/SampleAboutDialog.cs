using System;
using System.Reflection;
using GdkPixbuf;
using GObject;

namespace AboutDialog;

public class SampleAboutDialog : Gtk.AboutDialog
{
    public SampleAboutDialog(string sampleName)
    {
        Authors = new[] { "Gir.Core Developers", "badcel", "mjakeman" };
        Comments = "Gir.Core is a C# wrapper for GObject based libraries providing a C# friendly API surface.";
        Copyright = "© Gir.Core Developers 2021-present";
        License = "MIT License";
        Logo = LoadFromResource("AboutDialog.logo.svg");
        Version = "0.1.0";
        Website = "https://gircore.github.io/";
        LicenseType = Gtk.License.MitX11;
        ProgramName = $"{sampleName} - GirCore";
    }

    private static Gdk.Texture LoadFromResource(string resourceName)
    {
        try
        {
            var bytes = Assembly.GetExecutingAssembly().ReadResourceAsByteArray(resourceName);
            var pixbuf = PixbufLoader.FromBytes(bytes);
            return Gdk.Texture.NewForPixbuf(pixbuf);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unable to load image resource '{resourceName}': {e.Message}");
            return null;
        }
    }
}
