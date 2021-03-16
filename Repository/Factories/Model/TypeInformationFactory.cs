using Repository.Model;
using Repository.Xml;

namespace Repository.Factories.Model
{
    internal class TypeInformationFactory
    {
        private readonly ArrayFactory _arrayFactory;

        public TypeInformationFactory(ArrayFactory arrayFactory)
        {
            _arrayFactory = arrayFactory;
        }
        
        public TypeInformation CreateDefault()
        {
            return new TypeInformation(
                array: null,
                isPointer: false,
                isVolatile: false,
                isConst: false
            );
        }
        
        public TypeInformation Create(ITypeOrArray typeOrArray)
        {
            return new TypeInformation(
                array: _arrayFactory.Create(typeOrArray.Array),
                isPointer: IsPointer(typeOrArray),
                isVolatile: IsVolatile(typeOrArray),
                isConst: IsConst(typeOrArray)
            );
        }

        private bool IsPointer(ITypeOrArray typeOrArray)
        {
            if (typeOrArray.Array is { })
                return true; //Arrays are always pointers

            return GetIsPointer(typeOrArray.Type?.Name, typeOrArray.Type?.CType);
        }

        private bool GetIsPointer(string? type, string? ctype)
        {
            return (type, ctype) switch
            {
                ("utf8", _) => false,
                ("filename", _) => false,
                (_, "gpointer") => true,
                (_, {} c) => c.EndsWith("*"),
                _ => false
            };
        }

        private bool IsVolatile(ITypeOrArray typeOrArray)
        {
            if (typeOrArray.Array is { })
                return GetIsVolatile(typeOrArray.Array?.Type?.CType);

            return GetIsVolatile(typeOrArray.Type?.CType);
        }
        
        private bool GetIsVolatile(string? ctype)
            => ctype?.Contains("volatile") ?? false;

        private bool IsConst(ITypeOrArray typeOrArray)
        {
            if (typeOrArray.Array is { })
                return GetIsConst(typeOrArray.Array?.Type?.CType);

            return GetIsConst(typeOrArray.Type?.CType);
        }
        
        private bool GetIsConst(string? ctype)
            => ctype?.Contains("const") ?? false;
    }
}
