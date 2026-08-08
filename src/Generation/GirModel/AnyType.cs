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
           && arrayType is GirModel.GLibPtrArrayType;

    public static bool IsGLibByteArray(this GirModel.AnyType anyType)
        => anyType.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.GLibByteArrayType;

    public static bool IsGLibArray<T>(this GirModel.AnyType anyType) where T : GirModel.Type
        => anyType.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.GLibArrayType
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
           && arrayType is GirModel.GLibArrayType
           && arrayType.AnyType.IsAlias<T>();
}

namespace GirModel
{
    public class AnyType : OneOfBase<TypeReference, ArrayType>
    {
        private AnyType(OneOf<TypeReference, ArrayType> input) : base(input) { }

        public static AnyType From(TypeReference typeReference) => new(OneOf<TypeReference, ArrayType>.FromT0(typeReference));
        public static AnyType From(ArrayType arrayType) => new(OneOf<TypeReference, ArrayType>.FromT1(arrayType));
    }
}
