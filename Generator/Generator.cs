using System.Linq;
using System.Collections.Generic;

using Repository;
using StrongInject;

using Generator.Services;

namespace Generator
{
    public static class Generator
    {
        public static void Write(IEnumerable<string> projects, string outputDir = "output")
        {
            var repository = new Repository.Repository();
            var loadedProjects = repository.Load(FileResolver.ResolveFile, projects).ToList();

            Log.Information("Ready to write.");

            var classStructResolverService = new ClassStructResolverService();
            classStructResolverService.Resolve(loadedProjects);

            var writerService = new Container().Resolve().Value;

            foreach (LoadedProject proj in loadedProjects)
                writerService.Write(proj, outputDir);
                
            Log.Information("Writing completed successfully");
        }
    }
}
