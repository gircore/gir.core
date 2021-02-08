using Repository.Analysis;

namespace Repository.Model
{
    public class Argument : Symbol
    {
        public ISymbolReference SymbolReference { get; }
        public Direction Direction { get; }
        public Transfer Transfer { get; }
        public bool Nullable { get; }

        public Argument(string nativeName, string managedName, ISymbolReference symbolReference, Direction direction, Transfer transfer, bool nullable) : base(nativeName, managedName)
        {
            SymbolReference = symbolReference;
            Direction = direction;
            Transfer = transfer;
            Nullable = nullable;
        }
    }
}
