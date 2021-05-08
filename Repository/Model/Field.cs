using System.Collections.Generic;

namespace Repository.Model
{
    public class Field : Element, AnyType
    {
        public TypeReference TypeReference { get; }
        public TypeInformation TypeInformation { get; }

        public Callback? Callback { get; }
        public bool Readable { get; }
        public bool Private { get; }

        /// <summary>
        /// Creates a new field.
        /// </summary>
        /// <param name="symbolName"></param>
        /// <param name="typeReference"></param>
        /// <param name="typeInformation"></param>
        /// <param name="callback">Optional: If set it is expected that the callback belongs to the given symbol reference.</param>
        /// <param name="readable"></param>
        /// <param name="private"></param>
        /// <param name="elementName"></param>
        public Field(ElementName elementName, SymbolName symbolName, TypeReference typeReference, TypeInformation typeInformation, Callback? callback = null, bool readable = true, bool @private = false) : base(elementName, symbolName)
        {
            TypeReference = typeReference;
            TypeInformation = typeInformation;
            Callback = callback;
            Readable = readable;
            Private = @private;

            TryResolveSymbolReference();
        }

        private void TryResolveSymbolReference()
        {
            if (Callback is null)
                return;

            TypeReference.ResolveAs(Callback);
        }

        public override IEnumerable<TypeReference> GetTypeReferences()
        {
            // If the callback is not null this means the symbol reference
            // of this field was already resolved automatically. Meaning we
            // do not return the symbol reference to the caller but the
            // symbol references of the callback instead.
            //
            // If the callback is null the symbol reference is still unresolved
            // and must be returned to get resolved.

            if (Callback is not null)
                return Callback.GetTypeReferences();
            else
                return new List<TypeReference>() { TypeReference };
        }

        public override bool GetIsResolved()
            => TypeReference.GetIsResolved() && (Callback?.GetIsResolved() ?? true);
    }
}
