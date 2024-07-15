using System.Collections.Generic;
using System.Linq;

namespace GirLoader;

/// <summary>
/// Tries multiple repository resolvers, returning the first result found
/// </summary>
public class ChainedRepositoryResolver : IRepositoryResolver
{
    private readonly IReadOnlyCollection<IRepositoryResolver> _resolvers;

    public ChainedRepositoryResolver(IReadOnlyCollection<IRepositoryResolver> resolvers)
    {
        _resolvers = resolvers;
    }

    public Input.Repository? ResolveRepository(string fileName)
    {
        foreach (var resolver in _resolvers)
        {
            var repository = resolver.ResolveRepository(fileName);
            if (repository != null)
            {
                return repository;
            }
        }

        return null;
    }
}
