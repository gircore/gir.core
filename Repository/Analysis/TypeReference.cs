using Repository.Model;

namespace Repository.Analysis
{
    public enum ReferenceType
    {
        Internal,
        External
    }
    
    public class TypeReference
    {
        public ISymbol Type { get; private set; }
        public bool IsForeign { get; private set; }
        public bool IsArray { get; }

        internal string UnresolvedName { get; }
        internal bool IsResolved { get; private set; }

        public TypeReference(string unresolvedName, bool isArray)
        {
            UnresolvedName = unresolvedName;
            IsResolved = false;
            IsArray = isArray;
        }

        internal void ResolveAs(ISymbol symbol, ReferenceType type)
        {
            if (!IsResolved)
            {
                Type = symbol;
                IsForeign = (type == ReferenceType.External);
            }
            
            IsResolved = true;
        }
    }
}
