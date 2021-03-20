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
        public Scope CallbackScope { get; }
        public TypeInformation TypeInformation { get; }

        public Argument(ElementName elementName, ElementManagedName elementManagedName, SymbolReference symbolReference, Direction direction, Transfer transfer, bool nullable, int? closureIndex, int? destroyIndex, Scope scope, TypeInformation typeInformation) : base(elementName, elementManagedName)
        {
            SymbolReference = symbolReference;
            Direction = direction;
            Transfer = transfer;
            Nullable = nullable;
            ClosureIndex = closureIndex;
            DestroyIndex = destroyIndex;
            CallbackScope = scope;
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
