using System.Collections.Generic;
using Repository.Graph;
using Repository.Model;

#nullable enable

namespace Repository
{
    public interface ILoadedProject : INode<ILoadedProject>
    {
        Namespace Namespace { get; }
    }
    public class LoadedProject : ILoadedProject
    {
        public string Name { get; }
        public Namespace Namespace { get; }
        public IEnumerable<ILoadedProject> Dependencies { get; }

        public LoadedProject(string name, Namespace @namespace, IEnumerable<ILoadedProject> dependencies)
        {
            Name = name;
            Namespace = @namespace;
            Dependencies = dependencies;
        }
    }
}
