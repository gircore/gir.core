using System.Collections.Generic;

namespace Gir.Model
{
    public class Property : Element, TransferableAnyType
    {
        public Transfer Transfer { get; }
        public bool Writeable { get; }
        public bool Readable { get; }
        public TypeReference TypeReference { get; }
        public TypeInformation TypeInformation { get; }

        public Property(ElementName elementName, SymbolName symbolName, TypeReference typeReference, bool writeable, bool readable, Transfer transfer, TypeInformation typeInformation) : base(elementName, symbolName)
        {
            TypeReference = typeReference;
            Writeable = writeable;
            Transfer = transfer;
            TypeInformation = typeInformation;
            Readable = readable;
        }

        public override IEnumerable<TypeReference> GetTypeReferences()
        {
            yield return TypeReference;
        }

        public override bool GetIsResolved()
            => TypeReference.GetIsResolved();
    }
}
