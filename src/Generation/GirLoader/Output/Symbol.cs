using System.Collections.Generic;

namespace GirLoader.Output
{
    public abstract class Symbol : Named
    {
        //TODO: Remove property
        public SymbolName OriginalName { get; }
        public SymbolName Name { get; set; }

        protected Symbol(SymbolName originalName, SymbolName name)
        {
            OriginalName = originalName;
            Name = name;
        }

        public override string ToString()
            => Name;
    }
}
