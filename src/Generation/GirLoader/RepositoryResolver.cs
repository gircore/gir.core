using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader
{
    internal class RepositoryResolver
    {
        private readonly TypeReferenceResolver _typeReferenceResolver = new();
        private readonly HashSet<Output.Repository> _knownRepositories = new();

        /// <summary>
        /// Loads the given repository and all its dependencies
        /// </summary>
        public void Add(Output.Repository repository)
        {
            if (!_knownRepositories.Add(repository))
                return; //Ignore known repositories

            _typeReferenceResolver.AddRepository(repository);

            foreach (var dependentRepository in repository.Includes.Select(x => x.GetResolvedRepository()))
                Add(dependentRepository);
        }

        /// <summary>
        /// Resolves all loaded repositories
        /// </summary>
        public void Resolve()
        {
            var dependencyResolver = new Helper.DependencyResolver<Output.Repository>();
            var orderedRepositories = dependencyResolver.ResolveOrdered(_knownRepositories).Cast<Output.Repository>();

            foreach (var repository in orderedRepositories)
                ResolveTypeReferences(repository);
        }

        private void ResolveTypeReferences(Output.Repository repository)
        {
            foreach (var reference in repository.Namespace.GetTypeReferences())
                ResolveTypeReference(reference, repository);

            Log.Debug($"Resolved type references for repository {repository.Namespace.Name}");
        }

        private void ResolveTypeReference(Output.TypeReference reference, Output.Repository repository)
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
