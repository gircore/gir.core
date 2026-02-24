using System.Diagnostics.CodeAnalysis;
using OneOf;

public static class AnyTypeExtension
{
    public static bool Is<T>(this GirModel.AnyType anyType) where T : GirModel.Type
        => anyType.TryPickT0(out var type, out _) && type is T;

    public static bool Is<T>(this GirModel.AnyType anyType, [NotNullWhen(true)] out T? type) where T : class, GirModel.Type
    {
        var result = anyType.TryPickT0(out var t, out _) && t is T;
        if (result)
            type = (T) t;
        else
            type = null;

        return result;
    }

    public static bool IsArray<T>(this GirModel.AnyType anyType) where T : GirModel.Type
        => anyType.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.StandardArrayType
           && arrayType.AnyType.Is<T>();

    public static bool IsArray<T>(this GirModel.AnyType anyType, [NotNullWhen(true)] out T? type) where T : class, GirModel.Type
    {
        type = null;
        return anyType.TryPickT1(out var arrayType, out _)
               && arrayType is GirModel.StandardArrayType
               && arrayType.AnyType.Is(out type);
    }

    public static bool IsGLibPtrArray(this GirModel.AnyType anyType)
        => anyType.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.PointerArrayType;

    public static bool IsGLibByteArray(this GirModel.AnyType anyType)
        => anyType.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.ByteArrayType;

    public static bool IsGLibArray<T>(this GirModel.AnyType anyType) where T : GirModel.Type
        => anyType.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.GArrayType
           && arrayType.AnyType.Is<T>();

    public static bool IsAlias<T>(this GirModel.AnyType anyType) where T : GirModel.Type
        => anyType.TryPickT0(out var type, out _)
           && type is GirModel.Alias { Type: T };

    public static bool IsAlias<T>(this GirModel.AnyType anyType, [NotNullWhen(true)] out T? type) where T : class, GirModel.Type
    {
        var result = anyType.TryPickT0(out var t, out _) && t is GirModel.Alias { Type: T };

        if (result)
            type = (T) ((GirModel.Alias) t).Type;
        else
            type = null;

        return result;
    }

    public static bool IsArrayAlias<T>(this GirModel.AnyType anyType) where T : GirModel.Type
        => anyType.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.StandardArrayType
           && arrayType.AnyType.IsAlias<T>();

    public static bool IsGLibArrayAlias<T>(this GirModel.AnyType anyType) where T : GirModel.Type
        => anyType.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.GArrayType
           && arrayType.AnyType.IsAlias<T>();
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
