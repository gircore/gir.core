using System;
using System.Collections.Generic;
using Repository.Analysis;
using Repository.Xml;

namespace Repository.Services
{
    public interface ISymbolReferenceFactory
    {
        ISymbolReference Create(string type, bool isArray);
        ISymbolReference Create(ITypeOrArray typeOrArray);
        IEnumerable<ISymbolReference> Create(IEnumerable<ImplementInfo> implements);
        ISymbolReference? CreateWithNull(string? type, bool isArry);
    }

    public class SymbolReferenceFactory : ISymbolReferenceFactory
    {
        public ISymbolReference Create(string type, bool isArray)
        {
            return new SymbolReference(type, false);
        }
        
        public ISymbolReference Create(ITypeOrArray typeOrArray)
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

        public ISymbolReference? CreateWithNull(string? type, bool isArray)
        {
            return type is null ? null : Create(type, isArray);
        }
        
        public IEnumerable<ISymbolReference> Create(IEnumerable<ImplementInfo> implements)
        {
            var list = new List<ISymbolReference>();

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
