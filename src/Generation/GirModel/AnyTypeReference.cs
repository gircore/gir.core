using System;
using System.Diagnostics.CodeAnalysis;

namespace GirModel
{
    public union AnyTypeReference(TypeReference, ArrayTypeReference)
    {
        public TypeReference AsTypeReference => (TypeReference) Value;
        public ArrayTypeReference AsArrayTypeReference => (ArrayTypeReference) Value;
        
        public bool IsArrayTypeReference => this is ArrayTypeReference;

        public bool TryPickTypeReference([NotNullWhen(true)] out TypeReference? typeReference)
        {
            if(this is TypeReference reference)
            {
                typeReference = reference;
                return true;
            }

            typeReference = null;
            return false;
        }
        
        public bool TryPickTypeReference([NotNullWhen(true)] out TypeReference? typeReference, [NotNullWhen(false)] out ArrayTypeReference? arrayTypeReference)
        {
            if(this is TypeReference reference)
            {
                typeReference = reference;
                arrayTypeReference = null;
                return true;
            }

            typeReference = null;
            arrayTypeReference = (ArrayTypeReference) Value;
            return false;
        }
        
        public bool TryPickArrayTypeReference([NotNullWhen(true)] out ArrayTypeReference? arrayTypeReference)
        {
            if(this is ArrayTypeReference reference)
            {
                arrayTypeReference = reference;
                return true;
            }

            arrayTypeReference = null;
            return false;
        }
        
        public bool References<T>() where T : GirModel.Type
        {
            return this switch
            {
                TypeReference typeReference => typeReference.Type is T,
                ArrayTypeReference => false
            };
        }
        
        public bool References<T>([NotNullWhen(true)] out T? type) where T : class, GirModel.Type
        {
            if (this is ArrayTypeReference)
            {
                type = null;
                return false;
            }
            
            if(((TypeReference) Value).Type is T typeReference)
            {
                type = typeReference;
                return true;
            }

            throw new Exception("Unknown Type");
        }
        
        public bool ReferencesArray<T>() where T : GirModel.Type
        {
            return this switch
            {
                GirModel.StandardArrayTypeReference arrayTypeReference => arrayTypeReference.AnyTypeReference.References<T>(),
                _ => false,
            };
        }
        
        public bool ReferencesArray<T>([NotNullWhen(true)] out T? type) where T : class, GirModel.Type
        {
            type = null;

            return this switch
            {
                GirModel.StandardArrayTypeReference arrayTypeReference => arrayTypeReference.AnyTypeReference.References(out type),
                _ => false,
            };
        }
        
        public bool ReferencesGLibPtrArray()
        {
            return this switch
            {
                GirModel.GLibPtrArrayTypeReference => true,
                _ => false
            };
        }
        
        public bool ReferencesGLibByteArray()
        {
            return this switch
            {
                GirModel.GLibByteArrayTypeReference => true,
                _ => false
            };
        }
        
        public bool ReferencesGLibArray<T>() where T : GirModel.Type
        {
            return this switch
            {
                GirModel.GLibArrayTypeReference arrayTypeReference => arrayTypeReference.AnyTypeReference.References<T>(),
                _ => false
            };
        }
        
        public bool ReferencesAlias<T>() where T : GirModel.Type
        {
            return this switch
            {
                TypeReference typeReference => typeReference.Type is GirModel.Alias { Type: T },
                _ => false
            };
        }
        
        public bool ReferencesAlias<T>([NotNullWhen(true)] out T? type) where T : class, GirModel.Type
        {
            if (!ReferencesAlias<T>())
            {
                type = null;
                return false;
            }

            type = (T) ((GirModel.Alias) ((TypeReference) Value).Type).Type;
            return true;
        }
        
        public bool ReferencesArrayAlias<T>() where T : GirModel.Type
        {
            return this switch
            {
                GirModel.StandardArrayTypeReference arrayTypeReference => arrayTypeReference.AnyTypeReference.ReferencesAlias<T>(),
                _ => false
            };
        }
        
        public bool ReferencesGLibArrayAlias<T>() where T : GirModel.Type
        {
            return this switch
            {
                GirModel.GLibArrayTypeReference arrayTypeReference => arrayTypeReference.AnyTypeReference.ReferencesAlias<T>(),
                _ => false
            };
        }
    }
}
