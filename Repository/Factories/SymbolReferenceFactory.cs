using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Repository.Analysis;
using Repository.Model;
using Repository.Xml;
using Array = Repository.Model.Array;

namespace Repository.Services
{
    internal class SymbolReferenceFactory 
    {
        public SymbolReference Create(string? type, string? ctype, bool isPointer = false)
        {
            return new SymbolReference(type, ctype, isPointer);
        }

        public SymbolReference CreateFromField(FieldInfo field)
        {
            if (field.Callback is null)
                return Create(field);

            if (field.Callback.Name is null)
                throw new Exception($"Field {field.Name} has a callback without a name.");
            
            return Create(field.Callback.Name, field.Callback.Type);
        }
        
        public SymbolReference Create(ITypeOrArray typeOrArray)
        {
            if (TryCreate(typeOrArray?.Type, out var type))
                return type;
            
            if (TryCreate(typeOrArray?.Type, out var array))
                return array;
            
            return Create("void", "none");
        }
        
        private bool TryCreate(TypeInfo? typeInfo,  [MaybeNullWhen(false)] out SymbolReference symbolReference)
        {
            symbolReference = null;
            
            if (typeInfo is null)
                return false;
            
            symbolReference = new SymbolReference(typeInfo.Name, typeInfo.CType);
            return true;
        }

        public SymbolReference? CreateWithNull(string? type, string? ctype)
        {
            return type is null ? null : Create(type, ctype);
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
