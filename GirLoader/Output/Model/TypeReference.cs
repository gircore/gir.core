using System;

namespace GirLoader.Output.Model
{
    public abstract class TypeReference
    {
        #region Properties
        public CTypeReference? CTypeReference { get; }
        public SymbolNameReference? SymbolNameReference { get; }
        public abstract Type? Type { get; }

        #endregion

        protected TypeReference(SymbolNameReference? symbolNameReference, CTypeReference? ctypeReference)
        {
            CTypeReference = ctypeReference;
            SymbolNameReference = symbolNameReference;
        }

        public Type GetResolvedType()
        {
            if (Type is null)
                throw new InvalidOperationException($"The {GetType().Namespace} for {SymbolNameReference} has not been resolved.");

            return Type;
        }

        public override string ToString()
        {
            return $"{nameof(TypeReference)}: {nameof(CTypeReference)}: {CTypeReference}, {nameof(SymbolNameReference)}: {SymbolNameReference}";
        }

        internal bool GetIsResolved()
            => Type is { };
    }
}
