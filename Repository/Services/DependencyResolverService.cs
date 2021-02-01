using System;
using System.Collections.Generic;

#nullable enable

namespace Repository.Graph
{
    public interface IDependencyResolverService<T> where T : INode<T>
    {
        IEnumerable<INode<T>> ResolveOrdered(IEnumerable<INode<T>> nodeList);
    }

    // Dependency Resolver Algorithm
    // https://www.electricmonk.nl/docs/dependency_resolving_algorithm/dependency_resolving_algorithm.html
    public class DependencyResolverService<T> : IDependencyResolverService<T> where T : INode<T>
    {
        private List<INode<T>> _resolvedNodes = new ();
        private List<INode<T>> _unresolvedNodes = new ();

        public IEnumerable<INode<T>> ResolveOrdered(IEnumerable<INode<T>> nodeList)
        {
            _resolvedNodes = new List<INode<T>>();
            _unresolvedNodes = new List<INode<T>>();

            foreach (INode<T> node in nodeList)
            {
                if (!_resolvedNodes.Contains(node))
                    ResolveDependenciesRecursive(node);
            }

            return _resolvedNodes;
        }

        private void ResolveDependenciesRecursive(INode<T> node)
        {
            _unresolvedNodes.Add(node);

            foreach (T dep in node.Dependencies)
            {
                if (_resolvedNodes.Contains(dep))
                    continue;

                if (_unresolvedNodes.Contains(dep))
                    throw new Exception($"Recursive Dependencies: {node.Name} <-> {dep.Name}");

                // Recursively call this function
                ResolveDependenciesRecursive(dep);
            }

            _resolvedNodes.Add(node);
            _unresolvedNodes.Remove(node);
        }
    }
}
