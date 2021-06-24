using System;

namespace GirLoader.Output.Model
{
    public abstract class TypeReference : Resolveable
    {
        #region Properties
        public CTypeReference? CTypeReference { get; }
        public SymbolName? OriginalName { get; }
        public abstract Type? ResolvedType { get; }
        
        #endregion

        public TypeReference(SymbolName? originalName, CTypeReference? ctypeReference)
        {
            CTypeReference = ctypeReference;
            OriginalName = originalName;
        }
        
        public Type GetResolvedType()
        {
            if (ResolvedType is null)
                throw new InvalidOperationException($"The {GetType().Namespace} for {OriginalName} has not been resolved.");

            return ResolvedType;
        }

        public override string ToString()
        {
            return $"CType: {CTypeReference}, OriginalName: {OriginalName}";
        }

        public bool GetIsResolved()
            => ResolvedType is { };
    }
}
