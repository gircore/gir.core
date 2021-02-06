using Repository.Analysis;

namespace Repository.Model
{
    public class ReturnValue
    {
        public ISymbolReference SymbolReference { get; }

        public ReturnValue(ISymbolReference symbolReference)
        {
            SymbolReference = symbolReference;
        }
    }
}
