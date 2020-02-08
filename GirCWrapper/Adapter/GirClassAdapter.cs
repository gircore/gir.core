using System.Collections.Generic;
using CWrapper;

namespace Gir
{
    public class GirClassAdapter : GirAdapterBase, Class
    {
        private readonly GClass cls;
        private readonly string import;

        public string Name => FixName(cls.Name);
        public IEnumerable<Method> Methods 
        {
            get 
            { 
                foreach(var method in GetMethods(cls.Constructors, import))
                    yield return method;

                foreach(var method in GetMethods(cls.Methods, import))
                    yield return method;
            }
        }

        public GirClassAdapter(GClass cls, string import, TypeResolver typeResolver, IdentifierFixer identifierFixer) : base(typeResolver, identifierFixer)
        {
            this.cls = cls ?? throw new System.ArgumentNullException(nameof(cls));
            this.import = import ?? throw new System.ArgumentNullException(nameof(import));
        }
    }
}