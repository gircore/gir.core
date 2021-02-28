using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class BasicSymbol : Symbol
    {
        public BasicSymbol(string name, string managedName) : base(name, managedName)
        {
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
            => Enumerable.Empty<SymbolReference>();
    }
}
