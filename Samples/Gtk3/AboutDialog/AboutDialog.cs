using System;
using System.Reflection;
using GdkPixbuf;
using GObject;

namespace AboutDialog
{
    public class SampleDialog : Gtk.AboutDialog
    {
        // TODO: Turn this into a subclass when we support that again
        public static SampleDialog CreateDialog(string sampleName)
        {
            /*var dialog = new Gtk.AboutDialog();
            dialog.Authors = new[] {"Gir.Core Developers", "badcel", "mjakeman"};
            dialog.Comments = "Gir.Core is a C# wrapper for GObject based libraries providing a C# friendly API surface.";
            dialog.Copyright = "© Gir.Core Developers 2021-present";
            dialog.License = "MIT License";
            dialog.Logo = LoadFromResource("AboutDialog.logo.svg");
            dialog.Version = "0.1.0";
            dialog.Website = "https://gircore.github.io/";
            dialog.LicenseType = Gtk.License.MitX11;
            dialog.ProgramName = $"{sampleName} - GirCore";*/

            return new SampleDialog(sampleName);
        }

        public SampleDialog(string sampleName)
        {
            this.Authors = new[] {"Gir.Core Developers", "badcel", "mjakeman"};
            this.Comments = "Gir.Core is a C# wrapper for GObject based libraries providing a C# friendly API surface.";
            this.Copyright = "© Gir.Core Developers 2021-present";
            this.License = "MIT License";
            this.Logo = LoadFromResource("AboutDialog.logo.svg");
            this.Version = "0.1.0";
            this.Website = "https://gircore.github.io/";
            this.LicenseType = Gtk.License.MitX11;
            this.ProgramName = $"{sampleName} - GirCore";
        }
        

        private static Pixbuf LoadFromResource(string resourceName)
        {
            try
            {
                var bytes = Assembly.GetExecutingAssembly().ReadResourceAsByteArray(resourceName);
                return PixbufLoader.FromBytes(bytes);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to load image resource '{resourceName}': {e.Message}");
                return null;
            }
        }
    }
}
