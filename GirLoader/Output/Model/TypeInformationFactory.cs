using System;
using GirLoader.Input.Model;
using GirLoader.Output.Model;

namespace GirLoader.Output.Model
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

        public TypeInformation Create(Input.Model.AnyType anyType)
        {
            return new TypeInformation(
                array: _arrayFactory.Create(anyType.Array),
                isPointer: IsPointer(anyType),
                isVolatile: IsVolatile(anyType),
                isConst: IsConst(anyType)
            );
        }

        private bool IsPointer(Input.Model.AnyType anyType)
        {
            return anyType switch
            {
                { Type: { } t } => GetIsPointer(t.Name, t.CType),
                { Array: { Type: { } t } a } => GetIsPointer(string.Empty, a.CType) || GetIsPointer(t.Name, t.CType),
                { Array: { SubArray: { } } } => true,
                Input.Model.Field { Callback: { } } => false, //Callbacks are no pointer as they are handled as delegates
                _ => throw new Exception("Can not get pointer information from type: " + anyType)
            };
        }

        private bool GetIsPointer(string? type, string? ctype)
        {
            return (type, ctype) switch
            {
                ("utf8", _) => false,
                ("filename", _) => false,
                (_, "gpointer") => true,
                (_, { } c) => c.EndsWith("*"),
                _ => false
            };
        }

        private bool IsVolatile(Input.Model.AnyType anyType)
        {
            if (anyType.Array is { })
                return GetIsVolatile(anyType.Array?.Type?.CType);

            return GetIsVolatile(anyType.Type?.CType);
        }

        private bool GetIsVolatile(string? ctype)
            => ctype?.Contains("volatile") ?? false;

        private bool IsConst(Input.Model.AnyType anyType)
        {
            if (anyType.Array is { })
                return GetIsConst(anyType.Array?.Type?.CType);

            return GetIsConst(anyType.Type?.CType);
        }

        private bool GetIsConst(string? ctype)
            => ctype?.Contains("const") ?? false;
    }
}
