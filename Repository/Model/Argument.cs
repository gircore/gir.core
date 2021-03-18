using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Argument : Element, Type
    {
        public SymbolReference SymbolReference { get; }
        public Direction Direction { get; }
        public Transfer Transfer { get; }
        public bool Nullable { get; }
        public int? ClosureIndex { get; }
        public int? DestroyIndex { get; }
        public TypeInformation TypeInformation { get; }

        public Argument(ElementName elementName, SymbolName symbolName, SymbolReference symbolReference, Direction direction, Transfer transfer, bool nullable, int? closureIndex, int? destroyIndex, TypeInformation typeInformation) : base(elementName, symbolName)
        {
            SymbolReference = symbolReference;
            Direction = direction;
            Transfer = transfer;
            Nullable = nullable;
            ClosureIndex = closureIndex;
            DestroyIndex = destroyIndex;
            TypeInformation = typeInformation;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }

        public override bool GetIsResolved()
            => SymbolReference.GetIsResolved();
    }
}
