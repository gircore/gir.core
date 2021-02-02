#nullable enable

namespace Repository.Model
{
    public class Enumeration : BasicType
    {
        public bool HasFlags { get; }
     
        public Enumeration(Namespace @namespace, string nativeName, string managedName, bool hasFlags) : base(@namespace, nativeName, managedName)
        {
            HasFlags = hasFlags;
        }
    }
}
