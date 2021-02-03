using System.Collections.Generic;
using Repository.Analysis;
using Repository.Graph;
using Repository.Model;

namespace Repository
{
    public interface ILoadedProject : INode<ILoadedProject>
    {
        Namespace Namespace { get; }
        
        IEnumerable<ITypeReference> TypeReferences { get; }
    }

    public class LoadedProject : ILoadedProject
    {
        public string Name { get; }
        public Namespace Namespace { get; }
        
        public IEnumerable<ITypeReference> TypeReferences { get; }
        public IEnumerable<ILoadedProject> Dependencies { get; }

        public LoadedProject(string name, Namespace @namespace, IEnumerable<ILoadedProject> dependencies, IEnumerable<ITypeReference> references)
        {
            Name = name;
            Namespace = @namespace;
            Dependencies = dependencies;
            TypeReferences = references;
        }
    }
}
