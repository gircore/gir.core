using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output.Model
{
    public class PrimitiveType : Type
    {
        public PrimitiveType(CType cType, SymbolName symbolName) : base(cType, symbolName)
        {
        }
        
        public override IEnumerable<TypeReference> GetTypeReferences() 
            => Enumerable.Empty<TypeReference>();

        public override bool GetIsResolved() 
            => true;
    }
}
