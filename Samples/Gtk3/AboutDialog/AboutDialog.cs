using System;
using System.IO;
using System.Reflection;
using Gtk;
using GdkPixbuf;

namespace AboutDialog
{
    public class SampleDialog : Gtk.AboutDialog
    {
        public SampleDialog(string sampleName)
        {
            // Default Properties (can override with object initialiser)
            Artists = new[] {"Person1", "Person2"};
            Authors = new[] {"Gir.Core Developers"};
            Comments = "Some comment";
            // Copyright = "© Gir.Core Developers 2021-present";
            License = "MIT License";
            Logo = LoadFromResource("AboutDialog.logo.svg");
            Version = "0.1.0";
            Website = "https://gircore.github.io/";
            LicenseType = Gtk.License.MitX11;
            ProgramName = $"GirCore - {sampleName}";

            // TODO: We cannot use string properties at the moment
            SetCopyright("© Gir.Core Developers 2021-present");
        }

        private Pixbuf LoadFromResource(string resourceName)
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
