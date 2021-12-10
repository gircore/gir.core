using System.Collections.Generic;
using System.Linq;
using GirLoader.Helper;
using StrongInject;

namespace GirLoader
{
    public delegate Input.Repository? ResolveInclude(Output.Include include);

    public class Loader
    {
        private readonly RepositoryConverter _repositoryConverter;

        public Loader() : this(FileIncludeResolver.Resolve) { }

        public Loader(ResolveInclude includeResolver)
        {
            _repositoryConverter = new Container(includeResolver).Resolve().Value;
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

            var outputRepositories = inputRepositories.Select(_repositoryConverter.LoadRepository);
            Resolve(outputRepositories);

            return outputRepositories;
        }

        private static void Resolve(IEnumerable<Output.Repository> repositories)
        {
            var repositoryList = OrderRepositories(repositories);
            var repositoryResolver = InitializeRepositoryResolver(repositoryList);

            foreach (var repository in repositoryList)
            {
                repositoryResolver.ResolveAliases(repository);
                repositoryResolver.ResolveCallbacks(repository);
                repositoryResolver.ResolveClasses(repository);
                repositoryResolver.ResolveInterfaces(repository);
                repositoryResolver.ResolveRecords(repository);
                repositoryResolver.ResolveFunctions(repository);
                repositoryResolver.ResolveConstants(repository);
                repositoryResolver.ResolveUnions(repository);
            }
        }
        
        private static List<Output.Repository> OrderRepositories(IEnumerable<Output.Repository> repositories)
        {
            var dependencyResolver = new RepositoryDependencyResolver();
            return dependencyResolver.ResolveOrdered(repositories).ToList();
        }

        private static RepositoryResolver InitializeRepositoryResolver(IEnumerable<Output.Repository> repositories)
        {
            var repositoryResolver = new RepositoryResolver();

            foreach (var repository in repositories)
                repositoryResolver.Add(repository);

            return repositoryResolver;
        }
    }
}
