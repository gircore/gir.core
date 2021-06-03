using System;

namespace GirLoader.Output.Model
{
    public abstract class TypeReference : Resolveable
    {
        #region Properties

        public NamespaceName? NamespaceName { get; }
        public CType? CType { get; }
        public SymbolName? OriginalName { get; }
        public abstract Type? ResolvedType { get; }
        
        #endregion

        public TypeReference(SymbolName? originalName, CType? ctype, NamespaceName? namespaceName)
        {
            CType = ctype;
            OriginalName = originalName;
            NamespaceName = namespaceName;
        }
        
        public Type GetResolvedType()
        {
            if (ResolvedType is null)
                throw new InvalidOperationException($"The {GetType().Namespace} for {OriginalName} has not been resolved.");

            return ResolvedType;
        }

        public override string ToString()
        {
            return $"CType: {CType}, OriginalName: {OriginalName}";
        }

        public bool GetIsResolved()
            => ResolvedType is { };
    }
}
