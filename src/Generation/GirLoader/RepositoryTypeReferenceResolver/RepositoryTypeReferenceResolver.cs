using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader
{
    internal class RepositoryTypeReferenceResolver
    {
        private readonly TypeReferenceResolver _typeReferenceResolver = new();
        private readonly HashSet<Output.Repository> _knownRepositories = new();

        public RepositoryTypeReferenceResolver(IEnumerable<Output.Repository> repositories)
        {
            foreach(var repository in repositories)
            {
                Add(repository);
            }
        }
        
        /// <summary>
        /// Loads the given repository and all its dependencies
        /// </summary>
        private void Add(Output.Repository repository)
        {
            if (!_knownRepositories.Add(repository))
                return; //Ignore known repositories

            _typeReferenceResolver.AddRepository(repository);

            foreach (var dependentRepository in repository.Includes.Select(x => x.GetResolvedRepository()))
                Add(dependentRepository);
        }

        public void ResolveTypeReference(Output.TypeReference reference, Output.Repository repository)
        {
            if (reference is Output.ArrayTypeReference arrayTypeReference)
            {
                // Array type references are not resolved directly. Only their type get's resolved
                // because arrays are no type themself. They only provide structure.
                ResolveTypeReference(arrayTypeReference.TypeReference, repository);
            }
            else if (reference is Output.ResolveableTypeReference resolveableTypeReference)
            {
                if (_typeReferenceResolver.Resolve(resolveableTypeReference, repository, out var type))
                    resolveableTypeReference.ResolveAs(type);
                else
                    Log.Verbose($"Could not resolve type reference {reference}");
            }
            else
            {
                throw new Exception($"Unknown {nameof(Output.TypeReference)} {reference.GetType().Name}");
            }
        }
    }
}
