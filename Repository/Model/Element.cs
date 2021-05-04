using System.Collections.Generic;

namespace Repository.Model
{
    public abstract class Element : SymbolReferenceProvider, Resolveable
    {
        /// <summary>
        /// Original name of the element
        /// </summary>
        public ElementName Name { get; }
        public SymbolName SymbolName { get; set; }

        protected Element(ElementName name, SymbolName symbolName)
        {
            Name = name;
            SymbolName = symbolName;
        }

        public abstract IEnumerable<SymbolReference> GetSymbolReferences();
        public abstract bool GetIsResolved();

        public override string ToString()
            => SymbolName;
    }
}
