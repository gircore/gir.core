using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Property : Symbol, TransferableAnyType
    {
        public Transfer Transfer { get; }
        public bool Writeable { get; }
        public bool Readable { get; }
        public TypeReference TypeReference { get; }

        public Property(SymbolName originalName, SymbolName symbolName, TypeReference typeReference, bool writeable, bool readable, Transfer transfer) : base(originalName, symbolName)
        {
            TypeReference = typeReference;
            Writeable = writeable;
            Transfer = transfer;
            Readable = readable;
        }

        internal override IEnumerable<TypeReference> GetTypeReferences()
        {
            yield return TypeReference;
        }

        internal override bool GetIsResolved()
            => TypeReference.GetIsResolved();
    }
}
