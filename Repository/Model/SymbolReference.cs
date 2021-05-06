using System;

namespace Repository.Model
{
    public class SymbolReference : Resolveable
    {
        #region Properties

        public Symbol? Symbol { get; private set; }
        public NamespaceName? NamespaceName { get; }
        public CTypeName? CTypeName { get; }
        public TypeName? TypeName { get; }

        #endregion

        public SymbolReference(TypeName? typeName, CTypeName? ctypeName, NamespaceName? namespaceName)
        {
            CTypeName = ctypeName;
            TypeName = typeName;
            NamespaceName = namespaceName;
        }

        public Symbol GetSymbol()
        {
            if (Symbol is null)
                throw new InvalidOperationException($"The symbolreference for {TypeName} has not been resolved.");

            return Symbol;
        }

        public void ResolveAs(Symbol symbol)
        {
            Symbol = symbol;
        }

        public bool GetIsResolved()
            => Symbol is { };
    }
}
