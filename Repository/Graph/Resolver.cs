using System;
using System.Collections.Generic;

#nullable enable

namespace Repository.Graph
{
    public interface IResolver
    {
        IEnumerable<INode> ResolveOrdered(IEnumerable<INode> nodeList);
    }

    // Dependency Resolver Algorithm
    // https://www.electricmonk.nl/docs/dependency_resolving_algorithm/dependency_resolving_algorithm.html
    public class Resolver : IResolver
    {
        private List<INode> _resolvedNodes = new ();
        private List<INode> _unresolvedNodes = new ();

        public IEnumerable<INode> ResolveOrdered(IEnumerable<INode> nodeList)
        {
            _resolvedNodes = new List<INode>();
            _unresolvedNodes = new List<INode>();

            foreach (INode node in nodeList)
            {
                if (!_resolvedNodes.Contains(node))
                    ResolveDependenciesRecursive(node);
            }

            return _resolvedNodes;
        }

        private void ResolveDependenciesRecursive(INode node)
        {
            _unresolvedNodes.Add(node);

            foreach (INode dep in node.Dependencies)
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
