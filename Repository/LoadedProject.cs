using System.Collections.Generic;
using Repository.Analysis;
using Repository.Graph;
using Repository.Model;

namespace Repository
{
    public interface ILoadedProject : INode<ILoadedProject>
    {
        Namespace Namespace { get; }
        
        IEnumerable<ISymbolReference> SymbolReferences { get; }
    }

    public class LoadedProject : ILoadedProject
    {
        public string Name { get; }
        public Namespace Namespace { get; }
        
        public IEnumerable<ISymbolReference> SymbolReferences { get; }
        public IEnumerable<ILoadedProject> Dependencies { get; }

        public LoadedProject(string name, Namespace @namespace, IEnumerable<ILoadedProject> dependencies, IEnumerable<ISymbolReference> references)
        {
            Name = name;
            Namespace = @namespace;
            Dependencies = dependencies;
            SymbolReferences = references;
        }
    }
}
