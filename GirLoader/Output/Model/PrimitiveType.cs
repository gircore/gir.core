using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output.Model
{
    public class PrimitiveType : Type
    {
        public PrimitiveType(CTypeName cTypeName, SymbolName symbolName) : base(cTypeName, symbolName)
        {
        }
        
        public override IEnumerable<TypeReference> GetTypeReferences() 
            => Enumerable.Empty<TypeReference>();

        public override bool GetIsResolved() 
            => true;
    }
}
