using System.Collections.Generic;
using System.Linq;
using GirLoader.Helper;

namespace GirLoader
{
    public delegate Input.Repository? ResolveInclude(Output.Include include);

    public class Loader
    {
        private readonly ResolveInclude _includeResolver;

        public Loader(ResolveInclude includeResolver)
        {
            _includeResolver = includeResolver;
        }

        public static void EnableDebugOutput()
            => Log.EnableDebugOutput();

        public static void EnableVerboseOutput()
            => Log.EnableVerboseOutput();

        public IEnumerable<Output.Repository> Load(IEnumerable<Input.Repository> inputRepositories)
        {
            Log.Information($"Initialising with {inputRepositories.Count()} toplevel repositories");

            var outputRepositories = inputRepositories.CreateOutputRepositories(_includeResolver);
            var orderedOutputRepositories = outputRepositories.OrderByDependencies();
            orderedOutputRepositories.ResolveReferences();
            return orderedOutputRepositories;
        }
    }
}
