using Repository.Analysis;

namespace Generator.Services
{
    public static class TypeService
    {
        public static string PrintTypeIdentifier(SymbolReference symbol)
        {
            // External Type
            //if (symbol.IsForeign)
            //    return $"{symbol.Type.Namespace}.{symbol.Type.ManagedName}";

            // Internal Type
            return symbol.Symbol.ManagedName;
        }
    }
}
