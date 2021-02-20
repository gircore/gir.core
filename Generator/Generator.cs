using System;
using System.Collections.Generic;
using System.Linq;

using Repository;
using Generator.Services.Writer;

using StrongInject;

namespace Generator
{
    public static class Generator
    {
        public static void Write(IEnumerable<string> projects, string outputDir = "output")
        {
            var repository = new Repository.Repository();
            var loadedProjects = repository.Load(FileResolver.ResolveFile, projects).ToList();

            Log.Information("Ready to write.");

            WriterService writerService = new Container().Resolve().Value;
                
            foreach (LoadedProject proj in loadedProjects)
                writerService.Write(proj, outputDir);
                
            Log.Information("Writing completed successfully");
        }
    }
}
