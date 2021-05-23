using System.Collections.Generic;

namespace GirLoader.Output.Model
{
    public class ReturnValue : TransferableAnyType, TypeReferenceProvider
    {
        public Transfer Transfer { get; }
        public bool Nullable { get; }
        public TypeReference TypeReference { get; }
        public TypeInformation TypeInformation { get; }

        public ReturnValue(TypeReference typeReference, Transfer transfer, bool nullable, TypeInformation typeInformation)
        {
            TypeReference = typeReference;
            Transfer = transfer;
            Nullable = nullable;
            TypeInformation = typeInformation;
        }

        public IEnumerable<TypeReference> GetTypeReferences()
        {
            yield return TypeReference;
        }

        public bool GetIsResolved()
            => TypeReference.GetIsResolved();
    }
}
