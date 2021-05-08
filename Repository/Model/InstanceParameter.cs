using System.Collections.Generic;

namespace Repository.Model
{
    public class InstanceParameter : Element, Parameter
    {
        public TypeReference TypeReference { get; }
        public Direction Direction { get; }
        public Transfer Transfer { get; }
        public bool Nullable { get; }
        public bool CallerAllocates { get; }
        public TypeInformation TypeInformation { get; }

        public InstanceParameter(ElementName elementName, SymbolName symbolName, TypeReference typeReference, Direction direction, Transfer transfer, bool nullable, bool callerAllocates, TypeInformation typeInformation) : base(elementName, symbolName)
        {
            TypeReference = typeReference;
            Direction = direction;
            Transfer = transfer;
            Nullable = nullable;
            CallerAllocates = callerAllocates;
            TypeInformation = typeInformation;
        }

        public override IEnumerable<TypeReference> GetTypeReferences()
        {
            yield return TypeReference;
        }

        public override bool GetIsResolved()
            => TypeReference.GetIsResolved();
    }
}
