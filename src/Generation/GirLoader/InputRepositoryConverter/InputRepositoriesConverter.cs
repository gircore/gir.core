using System.Collections.Generic;
using System.Linq;
using StrongInject;

namespace GirLoader
{
    internal static class InputRepositoriesConverter
    {
        public static IEnumerable<Output.Repository> CreateOutputRepositories(this IEnumerable<Input.Repository> inputRepositories, ResolveInclude resolveInclude)
        {
            var repositoryFactory = new OutputRepositoryFactoryContainer().Resolve().Value;
            var repositoryConverter = new InputRepositoryConverter(resolveInclude, repositoryFactory);

            return inputRepositories.Select(repositoryConverter.CreateOutputRepository);
        }
    }
}
