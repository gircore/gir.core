using System;
using System.Collections.Generic;
using Repository.Analysis;
using Repository.Model;
using Repository.Xml;
using Array = Repository.Model.Array;

namespace Repository.Services
{
    internal class SymbolReferenceFactory 
    {
        public SymbolReference Create(string type, string? ctype = null)
        {
            return new SymbolReference(type, ctype);
        }

        public SymbolReference CreateFromField(FieldInfo field)
        {
            if (field.Callback is null)
                return Create(field);

            if (field.Callback.Name is null)
                throw new Exception($"Field {field.Name} has a callback without a name.");
            
            return Create(field.Callback.Name);
        }
        
        public SymbolReference Create(ITypeOrArray typeOrArray)
        {
            // Check for Type
            var type = typeOrArray?.Type?.Name;
            if (type != null)
                return Create(type, typeOrArray?.Type?.CType);

            // Check for Array
            var arrayName = typeOrArray?.Array?.Type?.Name;
            if (arrayName != null)
                return Create(arrayName, typeOrArray?.Array?.Type?.CType);

            // No Type (i.e. void)
            return Create("none");
        }

        public SymbolReference? CreateWithNull(string? type)
        {
            return type is null ? null : Create(type);
        }
        
        public IEnumerable<SymbolReference> Create(IEnumerable<ImplementInfo> implements)
        {
            var list = new List<SymbolReference>();

            foreach (ImplementInfo implement in implements)
            {
                if (implement.Name is null)
                    throw new Exception("Implement is missing a name");

                list.Add(Create(implement.Name));
            }

            return list;
        }
    }
}
