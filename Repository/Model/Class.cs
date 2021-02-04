using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Class : BasicType
    {
        public string CType { get; }
        public ITypeReference? Parent { get; }
        public IEnumerable<ITypeReference> Implements { get; }
        
        public IEnumerable<Method> Methods { get; }

        public Class(Namespace @namespace, string nativeName, string managedName, string ctype, ITypeReference? parent, IEnumerable<ITypeReference> implements, IEnumerable<Method> methods) : base(@namespace, nativeName, managedName)
        {
            Parent = parent;
            Implements = implements;
            CType = ctype;
            Methods = methods;
        }
    }
}
