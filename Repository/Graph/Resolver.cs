using System;
using System.Collections.Generic;

namespace Repository.Graph
{
    // Dependency Resolver Algorithm
    // https://www.electricmonk.nl/docs/dependency_resolving_algorithm/dependency_resolving_algorithm.html
    public class Resolver
    {
        // Nodes that have been resolved
        private List<INode> resolved = new List<INode>();

        // Nodes that have been seen and are not resolved
        private List<INode> unresolved = new List<INode>();

        // Return Dependency List
        public List<INode> GetOrderedList() => resolved;

        public Resolver(IEnumerable<INode> nodeList)
        {
            // Resolve all nodes in node list
            foreach (INode node in nodeList)
            {
                if (!resolved.Contains(node))
                    ResolveRecursive(node);
            }
        }

        // Recursively resolve dependencies
        private void ResolveRecursive(INode node)
        {
            unresolved.Add(node);

            // Iterate over dependencies
            foreach (INode dep in node.Dependencies)
            {
                // If in resolved, we can skip
                if (!resolved.Contains(dep))
                {
                    // Detect Circular Dependencies
                    if (unresolved.Contains(dep))
                        throw new Exception($"Recursive Dependencies: {node.Name} <-> {dep.Name}");

                    // Recursively call this function
                    ResolveRecursive(dep);
                }
            }
            
            // Add to our resolved list
            resolved.Add(node);

            unresolved.Remove(node);
        }
    }
}
