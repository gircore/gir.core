using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Interface : Type
    {
        public string CType { get; }
        public IEnumerable<ISymbolReference> Implements { get; }
        
        public Interface(Namespace @namespace, string nativeName, string managedName, string cType, IEnumerable<ISymbolReference> implements) : base(@namespace, nativeName, managedName)
        {
            CType = cType;
            Implements = implements;
        }

        public override IEnumerable<ISymbolReference> GetSymbolReferences()
            => Enumerable.Empty<ISymbolReference>();
    }
}
