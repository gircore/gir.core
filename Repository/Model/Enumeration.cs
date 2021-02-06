using System.Collections.Generic;

namespace Repository.Model
{
    public class Enumeration : Type
    {
        public bool HasFlags { get; }
        public IEnumerable<Member> Members { get; }
        
        public Enumeration(Namespace @namespace, string nativeName, string managedName, bool hasFlags, IEnumerable<Member> members) : base(@namespace, nativeName, managedName)
        {
            HasFlags = hasFlags;
            Members = members;
        }
    }
}
