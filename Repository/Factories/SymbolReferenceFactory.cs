using System;
using System.Collections.Generic;
using Repository.Analysis;
using Repository.Xml;

namespace Repository.Services
{
    public class SymbolReferenceFactory 
    {
        public SymbolReference Create(string type, bool isArray)
        {
            return new SymbolReference(type, false);
        }
        
        public SymbolReference Create(ITypeOrArray typeOrArray)
        {
            // Check for Type
            var type = typeOrArray?.Type?.Name ?? null;
            if (type != null)
                return Create(type, false);

            // Check for Array
            var array = typeOrArray?.Array?.Type?.Name ?? null;
            if (array != null)
                return Create(array, true);

            // No Type (i.e. void)
            return Create("none", false);
        }

        public SymbolReference? CreateWithNull(string? type, bool isArray)
        {
            return type is null ? null : Create(type, isArray);
        }
        
        public IEnumerable<SymbolReference> Create(IEnumerable<ImplementInfo> implements)
        {
            var list = new List<SymbolReference>();

            foreach (ImplementInfo implement in implements)
            {
                if (implement.Name is null)
                    throw new Exception("Implement is missing a name");

                list.Add(Create(implement.Name, false));
            }

            return list;
        }
    }
}
