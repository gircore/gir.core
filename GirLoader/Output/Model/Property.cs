using System.Collections.Generic;

namespace GirLoader.Output.Model
{
    public class Property : Symbol, TransferableAnyType
    {
        public Transfer Transfer { get; }
        public bool Writeable { get; }
        public bool Readable { get; }
        public TypeReference TypeReference { get; }
        public TypeInformation TypeInformation { get; }

        public Property(SymbolName originalName, SymbolName symbolName, TypeReference typeReference, bool writeable, bool readable, Transfer transfer, TypeInformation typeInformation) : base(originalName, symbolName)
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
