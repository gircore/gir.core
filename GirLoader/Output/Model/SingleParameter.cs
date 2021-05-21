using System.Collections.Generic;

namespace GirLoader.Output.Model
{
    public class SingleParameter : Element, Parameter
    {
        public TypeReference TypeReference { get; }
        public Direction Direction { get; }
        public Transfer Transfer { get; }
        public bool Nullable { get; }
        public bool CallerAllocates { get; }
        public int? ClosureIndex { get; }
        public int? DestroyIndex { get; }
        public Scope CallbackScope { get; }
        public TypeInformation TypeInformation { get; }

        public SingleParameter(ElementName elementName, SymbolName symbolName, TypeReference typeReference, Direction direction, Transfer transfer, bool nullable, bool callerAllocates, int? closureIndex, int? destroyIndex, Scope scope, TypeInformation typeInformation) : base(elementName, symbolName)
        {
            TypeReference = typeReference;
            Direction = direction;
            Transfer = transfer;
            Nullable = nullable;
            CallerAllocates = callerAllocates;
            ClosureIndex = closureIndex;
            DestroyIndex = destroyIndex;
            CallbackScope = scope;
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
