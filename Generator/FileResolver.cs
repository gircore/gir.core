using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Generator
{
    internal static class FileResolver
    {
        private const string CacheDir = "../gir-files";

        
        public static FileInfo ResolveFile(Repository.Model.Include include)
        {
            // We store GIR files in the format 'Gtk-3.0.gir'
            // where 'Gtk' is the namespace and '3.0' the version
            var filename = $"{include.Name}-{include.Version}.gir";

            if (File.Exists(filename))
                return new FileInfo(filename);

            if (CheckCacheDirectory(filename, out FileInfo? fileInfo))
                return fileInfo;

            throw new FileNotFoundException(
                $"Could not find file '{filename}' in the current directory or cache directory");
        }

        private static bool CheckCacheDirectory(string fileName, [NotNullWhen(true)] out FileInfo? fileInfo)
        {
            var altPath = Path.Combine(CacheDir, fileName);
            if (File.Exists(altPath))
            {
                fileInfo = new FileInfo(altPath);
                return true;
            }

            fileInfo = null;
            return false;
        }
    }
}
