using System.IO;

namespace GirLoader.Helper
{
    public class FileIncludeResolver
    {
        public static Input.Repository? Resolve(Output.Include include)
        {
            // We store GIR files in the format 'Gtk-3.0.gir'
            // where 'Gtk' is the namespace and '3.0' the version
            var filename = $"{include.Name}-{include.Version}.gir";

            return File.Exists(filename)
                ? new FileInfo(filename).OpenRead().DeserializeGirInputModel()
                : null;
        }
    }
}
