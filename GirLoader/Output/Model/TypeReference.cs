using System;

namespace Gir.Output.Model
{
    public class TypeReference : Resolveable
    {
        #region Properties

        public Type? ResolvedType { get; private set; }
        public NamespaceName? NamespaceName { get; }
        public CTypeName? CTypeName { get; }
        public TypeName? TypeName { get; }

        #endregion

        public TypeReference(TypeName? typeName, CTypeName? ctypeName, NamespaceName? namespaceName)
        {
            CTypeName = ctypeName;
            TypeName = typeName;
            NamespaceName = namespaceName;
        }

        public Type GetResolvedType()
        {
            if (ResolvedType is null)
                throw new InvalidOperationException($"The symbolreference for {TypeName} has not been resolved.");

            return ResolvedType;
        }

        public void ResolveAs(Type type)
        {
            ResolvedType = type;
        }

        public bool GetIsResolved()
            => ResolvedType is { };
    }
}
