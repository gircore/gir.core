using System;
using System.Collections.Generic;

namespace Gir.Helper
{
    internal interface Node<out T> where T : Node<T>
    {
        public IEnumerable<T> Dependencies { get; }
    }
    
    // Dependency Resolver Algorithm
    // https://www.electricmonk.nl/docs/dependency_resolving_algorithm/dependency_resolving_algorithm.html
    internal class DependencyResolver<T> where T : Node<T>
    {
        private List<Node<T>> _resolvedNodes = new();
        private List<Node<T>> _unresolvedNodes = new();

        public IEnumerable<Node<T>> ResolveOrdered(IEnumerable<Node<T>> nodeList)
        {
            _resolvedNodes = new List<Node<T>>();
            _unresolvedNodes = new List<Node<T>>();

            foreach (Node<T> node in nodeList)
            {
                if (!_resolvedNodes.Contains(node))
                    ResolveDependenciesRecursive(node);
            }

            return _resolvedNodes;
        }

        private void ResolveDependenciesRecursive(Node<T> node)
        {
            _unresolvedNodes.Add(node);

            foreach (T dep in node.Dependencies)
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
