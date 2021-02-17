using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Argument : Symbol
    {
        public ISymbolReference SymbolReference { get; }
        public Direction Direction { get; }
        public Transfer Transfer { get; }
        public bool Nullable { get; }
        public int ClosureIndex { get; }
        public int DestroyIndex { get; }

        public Argument(string nativeName, string managedName, ISymbolReference symbolReference, Direction direction, Transfer transfer, bool nullable, int closureIndex, int destroyIndex) : base(nativeName, managedName)
        {
            SymbolReference = symbolReference;
            Direction = direction;
            Transfer = transfer;
            Nullable = nullable;
            ClosureIndex = closureIndex;
            DestroyIndex = destroyIndex;
        }

        public override IEnumerable<ISymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }
    }
}
