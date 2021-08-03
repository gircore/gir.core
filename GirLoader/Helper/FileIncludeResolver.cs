using System.IO;
using GirLoader.Input;

namespace GirLoader.Helper
{
    public class FileIncludeResolver
    {
        public static Input.Model.Repository? Resolve(Output.Model.Include include)
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
