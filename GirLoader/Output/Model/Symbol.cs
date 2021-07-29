using System.Collections.Generic;

namespace GirLoader.Output.Model
{
    public abstract class Symbol : TypeReferenceProvider, Resolveable
    {
        public SymbolName OriginalName { get; }
        public SymbolName Name { get; set; }

        protected Symbol(SymbolName originalName, SymbolName name)
        {
            OriginalName = originalName;
            Name = name;
        }

        public abstract IEnumerable<TypeReference> GetTypeReferences();
        public abstract bool GetIsResolved();

        public override string ToString()
            => Name;
    }
}
