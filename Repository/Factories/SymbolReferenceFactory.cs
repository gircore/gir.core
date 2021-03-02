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
        public SymbolReference Create(string type, Model.Array? array)
        {
            return new SymbolReference(type, array);
        }

        public SymbolReference CreateFromField(FieldInfo field)
        {
            if (field.Callback is null)
                return Create(field);

            if (field.Callback.Name is null)
                throw new Exception($"Field {field.Name} has a callback without a name.");
            
            return Create(field.Callback.Name, null);
        }
        
        public SymbolReference Create(ITypeOrArray typeOrArray)
        {
            // Check for Type
            var type = typeOrArray?.Type?.Name ?? null;
            if (type != null)
                return Create(type, null);

            // Check for Array
            var arrayName = typeOrArray?.Array?.Type?.Name; //.Type?.Name ?? null;
            if (arrayName != null)
            {
                var lengthStr = typeOrArray?.Array?.Length;
                int? length = lengthStr is null ? null : int.Parse(lengthStr);
                
                var arrayData = new Array()
                {
                    Length = length
                };
                
                return Create(arrayName, arrayData);
            }

            // No Type (i.e. void)
            return Create("none", null);
        }

        public SymbolReference? CreateWithNull(string? type, Array? array)
        {
            return type is null ? null : Create(type, array);
        }
        
        public IEnumerable<SymbolReference> Create(IEnumerable<ImplementInfo> implements)
        {
            var list = new List<SymbolReference>();

            foreach (ImplementInfo implement in implements)
            {
                if (implement.Name is null)
                    throw new Exception("Implement is missing a name");

                list.Add(Create(implement.Name, null));
            }

            return list;
        }
    }
}
