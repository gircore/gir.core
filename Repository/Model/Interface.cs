using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Interface : Type
    {
        public string CType { get; }
        public Method GetTypeFunction { get; }
        public IEnumerable<ISymbolReference> Implements { get; }
        
        public IEnumerable<Method> Methods { get; }
        public IEnumerable<Method> Functions { get; }
        
        public Interface(Namespace @namespace, string nativeName, string managedName, string cType, IEnumerable<ISymbolReference> implements, IEnumerable<Method> methods, IEnumerable<Method> functions, Method getTypeFunction) : base(@namespace, nativeName, managedName)
        {
            CType = cType;
            Implements = implements;
            Methods = methods;
            Functions = functions;
            GetTypeFunction = getTypeFunction;
        }

        public override IEnumerable<ISymbolReference> GetSymbolReferences()
        {
            return IEnumerables.Concat(
                Implements,
                GetTypeFunction.GetSymbolReferences(),
                Methods.GetSymbolReferences(),
                Functions.GetSymbolReferences()
            );
        }
    }
}
