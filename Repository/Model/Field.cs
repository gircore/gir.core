using Repository.Analysis;

namespace Repository.Model
{
    public class Field : Symbol
    {
        public ISymbolReference SymbolReference { get; }
        
        public Field(string nativeName, string managedName, ISymbolReference symbolReference) : base(nativeName, managedName)
        {
            SymbolReference = symbolReference;
        }
    }
}
