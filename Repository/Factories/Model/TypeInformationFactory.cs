using System;
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
        
        public TypeInformation Create(Typed typed)
        {
            return new TypeInformation(
                array: _arrayFactory.Create(typed.Array),
                isPointer: IsPointer(typed),
                isVolatile: IsVolatile(typed),
                isConst: IsConst(typed)
            );
        }

        private bool IsPointer(Typed typed)
        {
            return typed switch
            {
                {Type: { } t} => GetIsPointer(t.Name, t.CType),
                {Array: {Type: { } t}} => GetIsPointer(t.Name, t.CType),
                {Array: {Array:{}}} => true,
                FieldInfo {Callback: {}} => false, //Callbacks are no pointer as they are handled as delegates
                _ => throw new Exception("Can not get pointer information from type: " + typed)
            };
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

        private bool IsVolatile(Typed typed)
        {
            if (typed.Array is { })
                return GetIsVolatile(typed.Array?.Type?.CType);

            return GetIsVolatile(typed.Type?.CType);
        }
        
        private bool GetIsVolatile(string? ctype)
            => ctype?.Contains("volatile") ?? false;

        private bool IsConst(Typed typed)
        {
            if (typed.Array is { })
                return GetIsConst(typed.Array?.Type?.CType);

            return GetIsConst(typed.Type?.CType);
        }
        
        private bool GetIsConst(string? ctype)
            => ctype?.Contains("const") ?? false;
    }
}
