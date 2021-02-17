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
        private ISymbol? _symbol;
        
        #region Properties
        
        public bool IsExternal { get; private set; }
        public bool IsArray { get; }
        public string Name { get; }

        #endregion
        
        public SymbolReference(string name, bool isArray)
        {
            Name = name;
            IsArray = isArray;
        }

        public ISymbol GetSymbol()
        {
            if(_symbol is null)
                throw new InvalidOperationException($"The symbolreference for {Name} has not been resolved.");

            return _symbol;
        }
        
        public void ResolveAs(ISymbol symbol, ReferenceType referenceType)
        {
            _symbol = symbol;
            IsExternal = (referenceType == ReferenceType.External);
        }
    }
}
