using System.Collections.Generic;
using Repository.Analysis;
using Repository.Graph;
using Repository.Model;

namespace Repository
{
    public class LoadedProject : INode<LoadedProject>
    {
        public string Name { get; }
        public Namespace Namespace { get; }
        
        public IEnumerable<LoadedProject> Dependencies { get; }

        public LoadedProject(string name, Namespace @namespace, IEnumerable<LoadedProject> dependencies)
        {
            Name = name;
            Namespace = @namespace;
            Dependencies = dependencies;
        }
    }
}
