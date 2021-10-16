using System;
using OneOf;

namespace GirModel
{
    public class AnyType : OneOfBase<Type, ArrayType>
    {
        private AnyType(OneOf<Type, ArrayType> input) : base(input){ }

        public static AnyType From(Type type) => new (OneOf<Type, ArrayType>.FromT0(type));
        public static AnyType From(ArrayType arrayType) => new (OneOf<Type, ArrayType>.FromT1(arrayType));
        
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
        
        public void VerifyArrayType<T>()
        {
            Switch(
                type => throw new Exception("Return type is not a array type but a type."),
                arrayType =>
                {
                    if (arrayType.Type is not T)
                        throw new Exception($"Return type must be of array type {typeof(T).FullName}");
                }
            );
        }
    }
}
