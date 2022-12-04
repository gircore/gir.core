using System;
using System.Collections.Generic;

namespace GirLoader.Helper;

// Dependency Resolver Algorithm
// https://www.electricmonk.nl/docs/dependency_resolving_algorithm/dependency_resolving_algorithm.html
internal class RepositoriesDependencyResolver
{
    private readonly IEnumerable<Output.Repository> _nodeList;
    private readonly List<Output.Repository> _resolvedNodes = new();
    private readonly List<Output.Repository> _unresolvedNodes = new();
    private bool _isResolved;

    public RepositoriesDependencyResolver(IEnumerable<Output.Repository> nodeList)
    {
        _nodeList = nodeList;
    }

    public List<Output.Repository> ResolveOrdered()
    {
        if (_isResolved)
            return _resolvedNodes;

        foreach (Output.Repository node in _nodeList)
        {
            if (!_resolvedNodes.Contains(node))
                ResolveDependenciesRecursive(node);
        }

        _isResolved = true;

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
