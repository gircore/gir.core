using System.Collections.Generic;

namespace GirLoader.Output
{
    public abstract class Symbol
    {
        public SymbolName OriginalName { get; }
        public SymbolName Name { get; set; } //TODO: Remove as all names should be defined by the generator

        protected Symbol(SymbolName originalName, SymbolName name)
        {
            OriginalName = originalName;
            Name = name;
        }

        internal abstract IEnumerable<TypeReference> GetTypeReferences();
        internal abstract bool GetIsResolved();

        public override string ToString()
            => Name;
    }
}
