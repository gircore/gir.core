using System;
using System.Collections.Generic;

namespace GirLoader;

internal class InputRepositoryConverter
{
    private readonly ResolveInclude _resolveInclude;
    private readonly Output.RepositoryFactory _repositoryFactory;
    private readonly Dictionary<string, Output.Repository> _knownRepositories = new();

    public InputRepositoryConverter(ResolveInclude resolveInclude, Output.RepositoryFactory repositoryFactory)
    {
        _resolveInclude = resolveInclude;
        _repositoryFactory = repositoryFactory;
    }

    public Output.Repository CreateOutputRepository(Input.Repository inputRepository)
    {
        if (_knownRepositories.TryGetValue(inputRepository.ToString(), out Output.Repository? repository))
            return repository;

        repository = Create(inputRepository);
        _knownRepositories[inputRepository.ToString()] = repository;

        return repository;
    }

    private Output.Repository Create(Input.Repository inputRepository)
    {
        var repository = _repositoryFactory.Create(inputRepository);
        ResolveIncludes(repository.Includes);
        Log.Debug($"Created repository {repository.Namespace.Name}.");
        return repository;
    }

    private void ResolveIncludes(IEnumerable<Output.Include> includes)
    {
        foreach (var include in includes)
            include.Resolve(CreateOutputRepository(include));
    }

    private Output.Repository CreateOutputRepository(Output.Include include)
    {
        var includeFileInfo = _resolveInclude(include) ?? throw new Exception($"Could not resolve include {include.Name}");

        return CreateOutputRepository(includeFileInfo);
    }
}
