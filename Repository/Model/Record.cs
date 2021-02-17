using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Record : Type
    {
        public Method? GetTypeFunction { get; }
        public IEnumerable<Method> Methods { get; }
        public IEnumerable<Method> Functions { get; }
        public SymbolReference? GLibClassStructFor { get; }

        public Record(Namespace @namespace, string nativeName, string managedName, SymbolReference? gLibClassStructFor, IEnumerable<Method> methods, IEnumerable<Method> functions, Method? getTypeFunction) : base(@namespace, nativeName, managedName)
        {
            GLibClassStructFor = gLibClassStructFor;
            Methods = methods;
            Functions = functions;
            GetTypeFunction = getTypeFunction;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            var symbolReferences = IEnumerables.Concat(
                Methods.GetSymbolReferences(),
                Functions.GetSymbolReferences()
            );

            if (GetTypeFunction is { })
                symbolReferences = symbolReferences.Concat(GetTypeFunction.GetSymbolReferences());
            
            if (GLibClassStructFor is { })
                symbolReferences = symbolReferences.Append(GLibClassStructFor);

            return symbolReferences;
        }
    }
}
