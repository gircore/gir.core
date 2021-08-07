using System.Collections.Generic;
using GirLoader.Helper;
using StrongInject;

namespace GirLoader
{
    public delegate Input.Model.Repository? ResolveInclude(Output.Model.Include include);

    [RegisterModule(typeof(Output.Module))]
    public partial class Loader : IContainer<Output.Loader>
    {
        //TODO: Use this method
        public static void EnableDebugOutput()
            => Log.EnableDebugOutput();

        //TODO: Use this method
        public static void EnableVerboseOutput()
            => Log.EnableVerboseOutput();

        public static ResolveInclude IncludeResolver { get; set; } = FileIncludeResolver.Resolve;

        public static IEnumerable<Output.Model.Repository> Load(IEnumerable<Input.Model.Repository> repositories)
        {
            return new Loader().Run(outputLoader => outputLoader.LoadRepositories(repositories));
        }

        private Loader() { }

        [Factory]
        internal ResolveInclude GetResolveInclude() => IncludeResolver;
    }
}
