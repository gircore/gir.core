// 1. Create Repository Object
// 2. Import GIR (Deserialise)

// Terminology
// ---
// Repository = Contains any number of namespaces
// Project = One single namespace and associated data

// FileResolverFunc is called when a dependency needs to be resolved
// default implementation => Search current directory

using System.IO;

namespace Repository
{
    static class Program
    {
        // TODO: Embed in Assembly?
        private static string _cacheDir = "../gir-files";
        
        private static FileInfo ResolveFile(string nspace, string version)
        {
            // Attempt to resolve dependencies
            
            // We store GIR files in the format 'Gtk-3.0.gir'
            // where 'Gtk' is the namespace and '3.0' the version
            var filename = $"{nspace}-{version}.gir";
            
            // Check current directory
            if (File.Exists(filename))
                return new FileInfo(filename);
            
            // Check cache directory
            var altPath = Path.Combine(_cacheDir, filename);
            if (File.Exists(altPath))
                return new FileInfo(altPath);
            
            // Fail
            throw new FileNotFoundException(
                $"Could not find file '{filename}' in the current directory or cache directory");
        }
        
        public static void Main()
        {
            // Hypothetical example usage

            var targets = new[]
            {
                new Target("Gst", "1.0")
            };
            
            var repo = new Repository(ResolveFile, targets);

            // foreach (Project project in repo.GetOrderedProjects())
            // {
            //     // Write fancy generation code here
            //     // This lets us write an entirely data-driven generator
            //     // File format is not considered
            // }
        }
    }
}


