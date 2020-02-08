using System.Collections.Generic;
using CWrapper;

namespace Gir
{
    public class GirDelegateAdapater : GirAdapterBase, Delegate
    {
        private readonly GCallback callback;

        public string Name => FixName(callback.Name);
        public string ReturnType => GetType(callback.ReturnValue!, false);
        public IEnumerable<Parameter> Parameters => GetParameters(callback.Parameters.GetParameters());

        public GirDelegateAdapater(GCallback callback, TypeResolver typeResolver, IdentifierFixer identifierFixer) : base(typeResolver, identifierFixer)
        {
            this.callback = callback ?? throw new System.ArgumentNullException(nameof(callback));
        }
    }
}