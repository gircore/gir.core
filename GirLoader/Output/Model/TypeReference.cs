using System;

namespace GirLoader.Output.Model
{
    public class TypeReference : Resolveable
    {
        #region Properties

        public Type? ResolvedType { get; private set; }
        public NamespaceName? NamespaceName { get; }
        public CTypeName? CTypeName { get; }
        public SymbolName? OriginalName { get; }

        #endregion

        public TypeReference(SymbolName? originalName, CTypeName? ctypeName, NamespaceName? namespaceName)
        {
            CTypeName = ctypeName;
            OriginalName = originalName;
            NamespaceName = namespaceName;
        }

        public Type GetResolvedType()
        {
            if (ResolvedType is null)
                throw new InvalidOperationException($"The {nameof(TypeReference)} for {OriginalName} has not been resolved.");

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
