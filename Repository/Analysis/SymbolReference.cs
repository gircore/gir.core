using System;
using Repository.Model;
using Array = Repository.Model.Array;

namespace Repository.Analysis
{
    public class SymbolReference
    {
        private Symbol? _symbol;

        #region Properties

        public NamespaceName? NamespaceName { get; }
        public CTypeName? CTypeName { get; }
        public TypeName? TypeName { get; }

        public bool IsResolved => _symbol is { };

        #endregion

        public SymbolReference(TypeName? typeName, CTypeName? ctypeName, NamespaceName? namespaceName)
        {
            CTypeName = ctypeName;
            TypeName = typeName;
            NamespaceName = namespaceName;
        }
        
        public Symbol GetSymbol()
        {
            if (_symbol is null)
                throw new InvalidOperationException($"The symbolreference for {TypeName} has not been resolved.");

            return _symbol;
        }

        public void ResolveAs(Symbol symbol)
        {
            _symbol = symbol;
        }
    }
}
