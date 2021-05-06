using System.Collections.Generic;

namespace Repository.Model
{
    public class InstanceParameter : Element, Parameter
    {
        public SymbolReference SymbolReference { get; }
        public Direction Direction { get; }
        public Transfer Transfer { get; }
        public bool Nullable { get; }
        public bool CallerAllocates { get; }
        public TypeInformation TypeInformation { get; }

        public InstanceParameter(ElementName elementName, SymbolName symbolName, SymbolReference symbolReference, Direction direction, Transfer transfer, bool nullable, bool callerAllocates, TypeInformation typeInformation) : base(elementName, symbolName)
        {
            SymbolReference = symbolReference;
            Direction = direction;
            Transfer = transfer;
            Nullable = nullable;
            CallerAllocates = callerAllocates;
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
