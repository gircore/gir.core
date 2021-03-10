using System;
using Repository.Model;
using Array = Repository.Model.Array;

namespace Repository.Analysis
{
    public enum ReferenceType
    {
        Internal,
        External
    }

    public class SymbolReference
    {
        private Symbol? _symbol;

        #region Properties

        public string Type { get; }
        private string? CType { get; }
        public bool IsPointer => CType?.EndsWith("*") ?? false;
        
        #endregion

        public SymbolReference(string type, string? ctype = null)
        {
            Type = type;
            CType = ctype;
        }

        public Symbol GetSymbol()
        {
            if (_symbol is null)
                throw new InvalidOperationException($"The symbolreference for {Type} has not been resolved.");

            return _symbol;
        }

        public void ResolveAs(Symbol symbol)
        {
            _symbol = symbol;
        }
    }
}
