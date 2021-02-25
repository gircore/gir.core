using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repository;
using StrongInject;

using Generator.Services;
using Generator.Services.Writer;

namespace Generator
{
    public static class Generator
    {
        public static void Write(IEnumerable<string> projects, string outputDir = "output")
        {
            var repository = new Repository.Repository();
            var loadedProjects = repository.Load(FileResolver.ResolveFile, projects).ToList();

            var classStructResolverService = new ClassStructResolverService();
            classStructResolverService.Resolve(loadedProjects);
            
            Log.Information("Ready to write.");

            WriterService writerService = new Container().Resolve().Value;

            var tasks = new List<Task>();
            foreach (LoadedProject proj in loadedProjects)
                tasks.Add(Task.Run(() => writerService.Write(proj, outputDir)));

            Task.WaitAll(tasks.ToArray());
                
            Log.Information("Writing completed successfully");
        }
    }
}
