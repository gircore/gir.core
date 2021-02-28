using System;
using Repository.Model;

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
        
        public bool IsExternal { get; private set; }
        public bool IsArray { get; }
        public string Type { get; }

        #endregion
        
        public SymbolReference(string type, bool isArray)
        {
            Type = type;
            IsArray = isArray;
        }

        public Symbol GetSymbol()
        {
            if(_symbol is null)
                throw new InvalidOperationException($"The symbolreference for {Type} has not been resolved.");

            return _symbol;
        }
        
        public void ResolveAs(Symbol symbol, ReferenceType referenceType)
        {
            _symbol = symbol;
            IsExternal = (referenceType == ReferenceType.External);
        }
    }
}
