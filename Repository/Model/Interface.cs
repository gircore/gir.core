using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Interface : Type
    {
        public string CType { get; }
        public Method GetTypeFunction { get; }
        public IEnumerable<SymbolReference> Implements { get; }
        
        public IEnumerable<Method> Methods { get; }
        public IEnumerable<Method> Functions { get; }
        
        public Interface(Namespace @namespace, string name, string managedName, string cType, IEnumerable<SymbolReference> implements, IEnumerable<Method> methods, IEnumerable<Method> functions, Method getTypeFunction) : base(@namespace, name, managedName)
        {
            CType = cType;
            Implements = implements;
            Methods = methods;
            Functions = functions;
            GetTypeFunction = getTypeFunction;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
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
