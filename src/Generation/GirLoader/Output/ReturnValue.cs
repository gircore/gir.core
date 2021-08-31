﻿using System.Collections.Generic;

namespace GirLoader.Output
{
    public class ReturnValue : Transferable, AnyType
    {
        public Transfer Transfer { get; }
        public bool Nullable { get; }
        public TypeReference TypeReference { get; }

        public ReturnValue(TypeReference typeReference, Transfer transfer, bool nullable)
        {
            TypeReference = typeReference;
            Transfer = transfer;
            Nullable = nullable;
        }

        public IEnumerable<TypeReference> GetTypeReferences()
        {
            yield return TypeReference;
        }

        public bool GetIsResolved()
            => TypeReference.GetIsResolved();
    }
}
