using System;
using OneOf;

public static class AnyTypeExtension
{
    public static bool Is<T>(this GirModel.AnyType anyType) where T : GirModel.Type
        => anyType.TryPickT0(out var type, out _) && type is T;
    public static bool IsArray<T>(this GirModel.AnyType anyType) where T : GirModel.Type
        => anyType.TryPickT1(out var arrayType, out _) && arrayType.AnyType.Is<T>();
}

namespace GirModel
{
    public class AnyType : OneOfBase<Type, ArrayType>
    {
        private AnyType(OneOf<Type, ArrayType> input) : base(input) { }

        public static AnyType From(Type type) => new(OneOf<Type, ArrayType>.FromT0(type));
        public static AnyType From(ArrayType arrayType) => new(OneOf<Type, ArrayType>.FromT1(arrayType));

        //TODO: Delete
        public void VerifyType<T>()
        {
            Switch(
                type =>
                {
                    if (type is not T)
                        throw new Exception($"Return type must be of type {typeof(T).FullName}");
                },
                arrayType => throw new Exception("Return type is not a regular type but an array.")
            );
        }

        //TODO: Delete
        public void VerifyArrayType<T>()
        {
            Switch(
                type => throw new Exception("Return type is not a array type but a type."),
                arrayType =>
                {
                    arrayType.AnyType.Switch(
                        ThrowIfNot<T>,
                        _ => throw new NotSupportedException("Arrays of arrays not yet supported")
                    );
                }
            );
        }

        private static void ThrowIfNot<T>(Type type)
        {
            if (type is not T)
                throw new Exception($"Return type must be of array type {typeof(T).FullName}");
        }
    }
}
