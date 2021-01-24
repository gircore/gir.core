using System;
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

        public override string ToString()
        {
            // TODO: More advanced type resolution logic?

            if (!IsResolved)
                throw new InvalidOperationException("The Type Reference has not been resolved. It cannot be printed.");
            
            // Fundamental Type
            if (Type.Namespace == null)
                return Type.ManagedName;
            
            // External Array
            if (IsForeign && IsArray)
                return $"{Type.Namespace}.{Type.ManagedName}[]";
            
            // External Type
            if (IsForeign)
                return $"{Type.Namespace}.{Type.ManagedName}";
            
            // Internal Array
            if (IsArray)
                return $"{Type.ManagedName}[]";
            
            // Internal Type
            return Type.ManagedName;
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
