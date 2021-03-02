using System;
using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Field : Symbol
    {
        public SymbolReference SymbolReference { get; }
        
        public Callback? Callback { get; }

        /// <summary>
        /// Creates a new field.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="managedName"></param>
        /// <param name="symbolReference"></param>
        /// <param name="callback">Optional: If set it is expected that the callback belongs to the given symbol reference.</param>
        public Field(string name, string managedName, SymbolReference symbolReference, Callback? callback = null) : base(name, managedName)
        {
            SymbolReference = symbolReference;
            Callback = callback;

            TryResolveSymbolReference();
        }

        private void TryResolveSymbolReference()
        {
            if (Callback is null)
                return;

            SymbolReference.ResolveAs(Callback);
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            // If the callback is not null this means the symbol reference
            // of this field was already resolved automatically. Meaning we
            // do not return the symbol reference to the caller but the
            // symbol references of the callback instead.
            //
            // If the callback is null the symbol reference is still unresolved
            // and must be returned to get resolved.
            
            if (Callback is not null)
                return Callback.GetSymbolReferences();
            else
                return new List<SymbolReference>() { SymbolReference };
        }
    }
}
