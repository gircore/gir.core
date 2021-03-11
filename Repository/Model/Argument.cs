using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Argument : Symbol, Type
    {
        public SymbolReference SymbolReference { get; }
        public Direction Direction { get; }
        public Transfer Transfer { get; }
        public bool Nullable { get; }
        public int? ClosureIndex { get; }
        public int? DestroyIndex { get; }
        public Array? Array { get; }

        public Argument(string name, string managedName, SymbolReference symbolReference, Direction direction, Transfer transfer, bool nullable, int? closureIndex, int? destroyIndex, Array? array = null) : base(name, name, managedName, managedName)
        {
            SymbolReference = symbolReference;
            Direction = direction;
            Transfer = transfer;
            Nullable = nullable;
            ClosureIndex = closureIndex;
            DestroyIndex = destroyIndex;
            Array = array;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            yield return SymbolReference;
        }

        internal override bool GetIsResolved()
            => SymbolReference.IsResolved;
    }
}
