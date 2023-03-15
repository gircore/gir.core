using OneOf;

public static class AnyTypeExtension
{
    public static bool Is<T>(this GirModel.AnyType anyType) where T : GirModel.Type
        => anyType.TryPickT0(out var type, out _) && type is T;
    public static bool IsArray<T>(this GirModel.AnyType anyType) where T : GirModel.Type
        => anyType.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.StandardArrayType
           && arrayType.AnyType.Is<T>();

    public static bool IsGLibPtrArray<T>(this GirModel.AnyType anyType) where T : GirModel.Type
        => anyType.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.PointerArrayType
           && arrayType.AnyType.Is<T>();

    public static bool IsGLibByteArray(this GirModel.AnyType anyType)
        => anyType.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.ByteArrayType;

    public static bool IsGLibArray<T>(this GirModel.AnyType anyType) where T : GirModel.Type
        => anyType.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.GArrayType
           && arrayType.AnyType.Is<T>();
}

namespace GirModel
{
    public class AnyType : OneOfBase<Type, ArrayType>
    {
        private AnyType(OneOf<Type, ArrayType> input) : base(input) { }

        public static AnyType From(Type type) => new(OneOf<Type, ArrayType>.FromT0(type));
        public static AnyType From(ArrayType arrayType) => new(OneOf<Type, ArrayType>.FromT1(arrayType));
    }
}
