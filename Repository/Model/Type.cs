using Repository.Analysis;

namespace Repository.Model
{
    public interface Type
    {
        SymbolReference SymbolReference { get; }
        Array? Array { get; }
    }
}
