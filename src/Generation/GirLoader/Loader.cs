using System.Collections.Generic;
using System.Linq;
using GirLoader.Helper;

namespace GirLoader
{
    public delegate Input.Repository? ResolveInclude(Output.Include include);

    public class Loader
    {
        private readonly ResolveInclude _includeResolver;

        public Loader() : this(FileIncludeResolver.Resolve) { }

        public Loader(ResolveInclude includeResolver)
        {
            _includeResolver = includeResolver;
        }

        //TODO: Use this method
        public void EnableDebugOutput()
            => Log.EnableDebugOutput();

        //TODO: Use this method
        public void EnableVerboseOutput()
            => Log.EnableVerboseOutput();

        public IEnumerable<Output.Repository> Load(IEnumerable<Input.Repository> inputRepositories)
        {
            Log.Information($"Initialising with {inputRepositories.Count()} toplevel repositories");

            var outputRepositories = inputRepositories.CreateOutputRepositories(_includeResolver);
            var orderedOutputRepositories = outputRepositories.OrderByDependencies();
            orderedOutputRepositories.ResolveTypeReferences();
            return orderedOutputRepositories;
        }
    }
}
