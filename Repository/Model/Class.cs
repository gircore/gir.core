using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Class : Type
    {
        public string CType { get; }
        public ISymbolReference? Parent { get; }
        public IEnumerable<ISymbolReference> Implements { get; }
        
        public IEnumerable<Method> Methods { get; }

        public Class(Namespace @namespace, string nativeName, string managedName, string ctype, ISymbolReference? parent, IEnumerable<ISymbolReference> implements, IEnumerable<Method> methods) : base(@namespace, nativeName, managedName)
        {
            Parent = parent;
            Implements = implements;
            CType = ctype;
            Methods = methods;
        }
    }
}
