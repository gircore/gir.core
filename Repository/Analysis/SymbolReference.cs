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

        #endregion

        public SymbolReference(string type)
        {
            Type = type;
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
