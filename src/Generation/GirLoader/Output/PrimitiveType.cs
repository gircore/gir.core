using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    public abstract class PrimitiveType : Type
    {
        protected PrimitiveType(CType ctype, TypeName typeName) : base(ctype, typeName)
        {
        }

        internal override IEnumerable<TypeReference> GetTypeReferences()
            => Enumerable.Empty<TypeReference>();

        internal override bool GetIsResolved()
            => true;
    }
}
