using System;
using System.IO;
using System.Reflection;
using Gtk;
using GdkPixbuf;

namespace AboutDialog
{
    public class SampleDialog
    {
        // TODO: Turn this into a subclass when we support that again
        public static Gtk.AboutDialog CreateDialog(string sampleName)
        {
            var dialog = new Gtk.AboutDialog();
            dialog.Authors = new[] {"Gir.Core Developers", "badcel", "mjakeman"};
            dialog.Comments = "Gir.Core is a C# wrapper for GObject based libraries providing a C# friendly API surface.";
            dialog.License = "MIT License";
            dialog.Logo = LoadFromResource("AboutDialog.logo.svg");
            dialog.Version = "0.1.0";
            dialog.Website = "https://gircore.github.io/";
            dialog.LicenseType = Gtk.License.MitX11;
            dialog.ProgramName = $"{sampleName} - GirCore";

            // TODO: We cannot use unicode in string properties at the moment
            dialog.SetCopyright("© Gir.Core Developers 2021-present");
            // dialog.Copyright = "© Gir.Core Developers 2021-present";

            return dialog;
        }

        private static Pixbuf LoadFromResource(string resourceName)
        {
            try
            {
                using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AboutDialog.logo.svg");
                var logoBytes = new byte[stream!.Length];
                stream.Read(logoBytes, 0, logoBytes.Length);
                return PixbufLoader.FromBytes(logoBytes);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to load image resource '{resourceName}': {e.Message}");
                return null;
            }
        }
    }
}
