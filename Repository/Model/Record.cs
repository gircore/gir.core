using Repository.Analysis;

#nullable enable

namespace Repository.Model
{
    public class Record : BasicType
    {
        public ITypeReference? GLibClassStructFor { get; }
        
        public Record(Namespace @namespace, string nativeName, string managedName, ITypeReference? gLibClassStructFor) : base(@namespace, nativeName, managedName)
        {
            GLibClassStructFor = gLibClassStructFor;
        }
    }
}
