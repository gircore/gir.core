using System.Collections.Generic;

namespace Repository.Model
{
    public class ReturnValue : AnyType, TypeReferenceProvider
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
