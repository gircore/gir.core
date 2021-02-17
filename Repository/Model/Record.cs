using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Record : Type
    {
        public IEnumerable<Method> Methods { get; }
        public IEnumerable<Method> Functions { get; }
        public ISymbolReference? GLibClassStructFor { get; }
        
        public Record(Namespace @namespace, string nativeName, string managedName, ISymbolReference? gLibClassStructFor, IEnumerable<Method> methods, IEnumerable<Method> functions) : base(@namespace, nativeName, managedName)
        {
            GLibClassStructFor = gLibClassStructFor;
            Methods = methods;
            Functions = functions;
        }

        public override IEnumerable<ISymbolReference> GetSymbolReferences()
        {
            if (GLibClassStructFor is { })
                yield return GLibClassStructFor;
        }
    }
}
