namespace GirLoader.Output;

public partial class ArrayTypeReference : GirModel.ArrayType
{
    int? GirModel.ArrayType.Length => Length;
    bool GirModel.ArrayType.IsZeroTerminated => IsZeroTerminated;
    int? GirModel.ArrayType.FixedSize => FixedSize;
    bool GirModel.ArrayType.IsPointer => TypeReference.CTypeReference?.IsPointer ?? false;
    bool GirModel.ArrayType.IsConst => TypeReference.CTypeReference?.IsConst ?? false;
    bool GirModel.ArrayType.IsVolatile => TypeReference.CTypeReference?.IsVolatile ?? false;
    GirModel.AnyType GirModel.ArrayType.AnyType => TypeReference switch
    {
        ArrayTypeReference arrayTypeReference => GirModel.AnyType.From(arrayTypeReference),
        _ => GirModel.AnyType.From(TypeReference.GetResolvedType())
    };
}
