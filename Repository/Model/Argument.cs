using Repository.Analysis;

namespace Repository.Model
{
    public class Argument
    {
        public string Name { get; }
        public ISymbolReference SymbolReference { get; }
        public Direction Direction { get; }
        public Transfer Transfer { get; }
        public bool Nullable { get; }

        public Argument(string name, ISymbolReference symbolReference, Direction direction, Transfer transfer, bool nullable)
        {
            Name = name;
            SymbolReference = symbolReference;
            Direction = direction;
            Transfer = transfer;
            Nullable = nullable;
        }
    }
}
