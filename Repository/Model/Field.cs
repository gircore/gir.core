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
        /// <param name="nativeName"></param>
        /// <param name="managedName"></param>
        /// <param name="symbolReference"></param>
        /// <param name="callback">Optional: If set it is expected that the callback belongs to the given symbol reference.</param>
        public Field(string nativeName, string managedName, SymbolReference symbolReference, Callback? callback = null) : base(nativeName, managedName)
        {
            SymbolReference = symbolReference;
            Callback = callback;

            TryResolveSymbolReference();
        }

        private void TryResolveSymbolReference()
        {
            if (Callback is null)
                return;

            if (Callback.ManagedName == SymbolReference.Name)
                SymbolReference.ResolveAs(Callback, ReferenceType.Internal);
            else
                throw new Exception($"Field {ManagedName} should be a callback type. But the names do not match: {Callback.ManagedName} != {SymbolReference.Name}.");
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
