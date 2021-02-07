using Repository.Analysis;

namespace Repository.Model
{
    public class Property : Symbol
    {
        public Transfer Transfer { get; }
        public bool Writeable { get; }
        public ISymbolReference SymbolReference { get; }
        
        public Property(string nativeName, string managedName, ISymbolReference symbolReference, bool writeable, Transfer transfer) : base(nativeName, managedName)
        {
            SymbolReference = symbolReference;
            Writeable = writeable;
            Transfer = transfer;
        }
    }
}
