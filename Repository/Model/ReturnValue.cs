using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class ReturnValue : Type, SymbolReferenceProvider
    {
        public Transfer Transfer { get; }
        public bool Nullable { get; }
        public SymbolReference SymbolReference { get; }
        public TypeInformation TypeInformation { get; }

        public ReturnValue(SymbolReference symbolReference, Transfer transfer, bool nullable, TypeInformation typeInformation)
        {
            SymbolReference = symbolReference;
            Transfer = transfer;
            Nullable = nullable;
            TypeInformation = typeInformation;
        }

        public IEnumerable<SymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }

        public bool GetIsResolved() 
            => SymbolReference.GetIsResolved();
    }
}
