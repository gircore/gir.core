using System.Diagnostics.CodeAnalysis;
using OneOf;

public static class AnyTypeRefernceExtension
{
    public static bool References<T>(this GirModel.AnyTypeReference anyTypeReference) where T : GirModel.Type
        => anyTypeReference.TryPickT0(out var typeReference, out _) && typeReference.Type is T;

    public static bool References<T>(this GirModel.AnyTypeReference anyTypeReference, [NotNullWhen(true)] out T? type) where T : class, GirModel.Type
    {
        var result = anyTypeReference.TryPickT0(out var typeReference, out _) && typeReference.Type is T;
        if (result)
            type = (T) typeReference.Type;
        else
            type = null;

        return result;
    }

    public static bool ReferencesArray<T>(this GirModel.AnyTypeReference anyTypeReference) where T : GirModel.Type
        => anyTypeReference.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.StandardArrayTypeReference
           && arrayType.AnyTypeReference.References<T>();

    public static bool ReferencesArray<T>(this GirModel.AnyTypeReference anyTypeReference, [NotNullWhen(true)] out T? type) where T : class, GirModel.Type
    {
        type = null;
        return anyTypeReference.TryPickT1(out var arrayType, out _)
               && arrayType is GirModel.StandardArrayTypeReference
               && arrayType.AnyTypeReference.References(out type);
    }

    public static bool ReferencesGLibPtrArray(this GirModel.AnyTypeReference anyTypeReference)
        => anyTypeReference.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.GLibPtrArrayTypeReference;

    public static bool ReferencesGLibByteArray(this GirModel.AnyTypeReference anyTypeReference)
        => anyTypeReference.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.GLibByteArrayTypeReference;

    public static bool ReferencesGLibArray<T>(this GirModel.AnyTypeReference anyTypeReference) where T : GirModel.Type
        => anyTypeReference.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.GLibArrayTypeReference
           && arrayType.AnyTypeReference.References<T>();

    public static bool ReferencesAlias<T>(this GirModel.AnyTypeReference anyTypeReference) where T : GirModel.Type
        => anyTypeReference.TryPickT0(out var typeReference, out _)
           && typeReference.Type is GirModel.Alias { Type: T };

    public static bool ReferencesAlias<T>(this GirModel.AnyTypeReference anyTypeReference, [NotNullWhen(true)] out T? type) where T : class, GirModel.Type
    {
        var result = anyTypeReference.TryPickT0(out var typeReference, out _) && typeReference.Type is GirModel.Alias { Type: T };

        if (result)
            type = (T) ((GirModel.Alias) typeReference.Type).Type;
        else
            type = null;

        return result;
    }

    public static bool ReferencesArrayAlias<T>(this GirModel.AnyTypeReference anyTypeReference) where T : GirModel.Type
        => anyTypeReference.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.StandardArrayTypeReference
           && arrayType.AnyTypeReference.ReferencesAlias<T>();

    public static bool ReferencesGLibArrayAlias<T>(this GirModel.AnyTypeReference anyTypeReference) where T : GirModel.Type
        => anyTypeReference.TryPickT1(out var arrayType, out _)
           && arrayType is GirModel.GLibArrayTypeReference
           && arrayType.AnyTypeReference.ReferencesAlias<T>();
}

namespace GirModel
{
    public class AnyTypeReference : OneOfBase<TypeReference, ArrayTypeReference>
    {
        private AnyTypeReference(OneOf<TypeReference, ArrayTypeReference> input) : base(input) { }

        public static AnyTypeReference From(TypeReference typeReference) => new(OneOf<TypeReference, ArrayTypeReference>.FromT0(typeReference));
        public static AnyTypeReference From(ArrayTypeReference arrayTypeReference) => new(OneOf<TypeReference, ArrayTypeReference>.FromT1(arrayTypeReference));
    }
}
