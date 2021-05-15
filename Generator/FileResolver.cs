using System.Diagnostics.CodeAnalysis;
using System.IO;
using Gir;
using File = Gir.File;

namespace Generator
{
    internal static class FileResolver
    {
        private const string CacheDir = "../gir-files";

        
        public static File ResolveFile(Gir.Model.Include include)
        {
            // We store GIR files in the format 'Gtk-3.0.gir'
            // where 'Gtk' is the namespace and '3.0' the version
            var filename = $"{include.Name}-{include.Version}.gir";

            if (System.IO.File.Exists(filename))
                return new File(filename);

            if (CheckCacheDirectory(filename, out File? fileInfo))
                return fileInfo;

            throw new FileNotFoundException(
                $"Could not find file '{filename}' in the current directory or cache directory");
        }

        private static bool CheckCacheDirectory(string fileName, [NotNullWhen(true)] out File? fileInfo)
        {
            var altPath = Path.Combine(CacheDir, fileName);
            if (System.IO.File.Exists(altPath))
            {
                fileInfo = new File(altPath);
                return true;
            }

            fileInfo = null;
            return false;
        }
    }
}
