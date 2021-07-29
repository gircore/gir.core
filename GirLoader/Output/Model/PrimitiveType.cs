using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output.Model
{
    public abstract class PrimitiveType : Type
    {
        protected PrimitiveType(CType ctype, TypeName typeName) : base(null, ctype, typeName)
        {
        }

        public override IEnumerable<TypeReference> GetTypeReferences()
            => Enumerable.Empty<TypeReference>();

        public override bool GetIsResolved()
            => true;
    }
}
