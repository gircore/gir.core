using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repository;
using StrongInject;

using Generator.Services;
using Generator.Services.Writer;
using Repository.Model;

namespace Generator
{
    public class Generator
    {
        public string OutputDir { get; init; } = "output";
        public bool UseAsync { get; init; } = true;
        public bool GenerateMethods { get; init; } = false;
        public bool GenerateDocComments { get; init; } = false;

        public void Write(IEnumerable<string> projects)
        {
            var repository = new Repository.Repository();
            var namespaces = repository.Load(FileResolver.ResolveFile, projects).ToList();

            var typeRenamer = new TypeRenamer();
            typeRenamer.SetMetadata(namespaces);
            typeRenamer.FixPropertiesNamedLikeClass(namespaces);

            WriterService writerService = new Container().Resolve().Value;
            
            // Set writer options
            var options = new WriterOptions
            {
                GenerateMethods = GenerateMethods,
                GenerateDocComments = GenerateDocComments,
            };

            Log.Information("Optional Writer Properties:");
            Log.Information($" - Generating Managed Methods: {options.GenerateMethods}");
            Log.Information($" - Generating LGPL Documentation (not implemented yet): {options.GenerateDocComments}");

            Log.Information("Ready to write.");

            if (UseAsync)
            {
                Parallel.ForEach(namespaces,
                    proj => writerService.Write(proj, OutputDir, options));
            }
            else
            {
                Log.Warning("Async Generation is disabled. Generation may be slower than normal.");
                
                // Disable asynchronous writing for an easier debugging experience
                foreach (Namespace ns in namespaces)
                    writerService.Write(ns, OutputDir, options);
            }

            Log.Information("Writing completed successfully");
        }
    }
}
