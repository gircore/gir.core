﻿using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public abstract class Element : SymbolReferenceProvider, Resolveable
    {
        public ElementName Name { get; }
        public SymbolName SymbolName { get; }
        
        protected Element(ElementName name, SymbolName symbolName)
        {
            Name = name;
            SymbolName = symbolName;
        }

        public abstract IEnumerable<SymbolReference> GetSymbolReferences();
        public abstract bool GetIsResolved();
    }
}