using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public abstract class Element : SymbolReferenceProvider, Resolveable
    {
        public ElementName Name { get; }
        public ElementManagedName ManagedName { get; }
        
        protected Element(ElementName name, ElementManagedName managedName)
        {
            Name = name;
            ManagedName = managedName;
        }

        public abstract IEnumerable<SymbolReference> GetSymbolReferences();
        public abstract bool GetIsResolved();
    }
}
