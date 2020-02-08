using System.Collections.Generic;
using CWrapper;

namespace Gir
{
    public class GirInterfaceAdapter : GirAdapterBase, Class
    {
        private readonly GInterface iface;
        private readonly string import;

        public string Name => FixName(iface.Name);
        public IEnumerable<Method> Methods 
        {
            get 
            { 
                foreach(var method in GetMethods(iface.Methods, import))
                    yield return method;
            }
        }

        public GirInterfaceAdapter(GInterface iface, string import, TypeResolver typeResolver, IdentifierFixer identifierFixer) : base(typeResolver, identifierFixer)
        {
            this.iface = iface ?? throw new System.ArgumentNullException(nameof(iface));
            this.import = import ?? throw new System.ArgumentNullException(nameof(import));
        }
    }
}