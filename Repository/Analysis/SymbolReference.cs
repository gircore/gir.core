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

    /*
     * TODO
     * A SymbolReference should only reference a symbol and do not carry more information.
     *
     * Therefor this class should be cleaned up:
     * - Remove IsExternal property
     * - Remove Array property
     *
     * If those properties are removed the code in the TemplateWriter class can be
     * further cleaned up until the point that the whole TemplateWriter class get's
     * obsolete.
     */
    public class SymbolReference
    {
        private Symbol? _symbol;
        
        #region Properties
        
        //TODO
        //This is some kind of helper property, which should not be needed.
        //If a symbol is printed it can be checked if the symbol is in the current
        //namespace or not or other code can be executed. Currently this check is done beforehand.
        //This change probably means that every symbol _needs_ its namespace (except for basic symbols).
        public bool IsExternal { get; private set; }
        public string Type { get; }
        
        //TODO: The information if this symbol is an array or not should not be part
        //of the symbol reference as it is another topic. The property
        //should be part of the ReturnValue and Argument class
        public Array? Array { get; }

        #endregion
        
        public SymbolReference(string type, Array? array)
        {
            Type = type;
            Array = array;
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
