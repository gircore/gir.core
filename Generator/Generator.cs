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

            try
            {
                WriterService writerService = new Container().Resolve().Value;
                
                foreach (LoadedProject proj in loadedProjects)
                    writerService.Write(proj, outputDir);
                
                Log.Information("Writing completed successfully");
            }
            catch (Exception e)
            {
                Log.Exception(e);
                Log.Error("An error occurred while writing files. Please save a copy of your log output and open an issue at: https://github.com/gircore/gir.core/issues/new");
            }
        }
    }
}
