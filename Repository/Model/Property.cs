using System.Collections.Generic;

namespace Repository.Model
{
    public class Property : Element, TransferableType
    {
        public Transfer Transfer { get; }
        public bool Writeable { get; }
        public bool Readable { get; }
        public SymbolReference SymbolReference { get; }
        public TypeInformation TypeInformation { get; }

        public Property(ElementName elementName, SymbolName symbolName, SymbolReference symbolReference, bool writeable, bool readable, Transfer transfer, TypeInformation typeInformation) : base(elementName, symbolName)
        {
            SymbolReference = symbolReference;
            Writeable = writeable;
            Transfer = transfer;
            TypeInformation = typeInformation;
            Readable = readable;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }

        public override bool GetIsResolved()
            => SymbolReference.GetIsResolved();
    }
}
