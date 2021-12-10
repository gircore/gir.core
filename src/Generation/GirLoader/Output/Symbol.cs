using System.Collections.Generic;

namespace GirLoader.Output
{
    public abstract class Symbol
    {
        public SymbolName OriginalName { get; }

        protected Symbol(SymbolName originalName)
        {
            OriginalName = originalName;
        }

        internal abstract IEnumerable<TypeReference> GetTypeReferences();
        internal abstract bool GetIsResolved();
    }
}
