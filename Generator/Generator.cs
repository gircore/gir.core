using System.Linq;
using Repository;
using StrongInject;

namespace Generator
{
    public static class Generator
    {
        public static void Write(string[] projects)
        {
            var repository = new Repository.Repository();
            var loadedProjects = repository.Load(FileResolver.ResolveFile, projects).ToList();

            Log.Information("Ready to Write");

            var writerService = new Container().Resolve().Value;
            foreach (LoadedProject proj in loadedProjects)
            {
                writerService.Write(proj);
            }

            Log.Information("Writing completed successfully");
        }
    }
}
