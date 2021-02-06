using Repository.Analysis;

namespace Repository.Model
{
    public class Record : Type
    {
        public ISymbolReference? GLibClassStructFor { get; }
        
        public Record(Namespace @namespace, string nativeName, string managedName, ISymbolReference? gLibClassStructFor) : base(@namespace, nativeName, managedName)
        {
            GLibClassStructFor = gLibClassStructFor;
        }
    }
}
