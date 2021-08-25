using System;
using System.Collections.Generic;

namespace GirLoader.Helper
{
    // Dependency Resolver Algorithm
    // https://www.electricmonk.nl/docs/dependency_resolving_algorithm/dependency_resolving_algorithm.html
    internal class RepositoryDependencyResolver
    {
        private List<Output.Repository> _resolvedNodes = new();
        private List<Output.Repository> _unresolvedNodes = new();

        public IEnumerable<Output.Repository> ResolveOrdered(IEnumerable<Output.Repository> nodeList)
        {
            _resolvedNodes = new List<Output.Repository>();
            _unresolvedNodes = new List<Output.Repository>();

            foreach (Output.Repository node in nodeList)
            {
                if (!_resolvedNodes.Contains(node))
                    ResolveDependenciesRecursive(node);
            }

            return _resolvedNodes;
        }

        private void ResolveDependenciesRecursive(Output.Repository node)
        {
            _unresolvedNodes.Add(node);

            foreach (Output.Repository dep in node.Dependencies)
            {
                if (_resolvedNodes.Contains(dep))
                    continue;

                if (_unresolvedNodes.Contains(dep))
                    throw new Exception($"Recursive Dependencies: {node} <-> {dep}");

                // Recursively call this function
                ResolveDependenciesRecursive(dep);
            }

            _resolvedNodes.Add(node);
            _unresolvedNodes.Remove(node);
        }
    }
}
