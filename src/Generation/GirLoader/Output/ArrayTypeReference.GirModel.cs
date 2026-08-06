namespace GirLoader.Output;

public partial class ArrayTypeReference : GirModel.ArrayType
{
    int? GirModel.ArrayType.Length => Length;
    bool GirModel.ArrayType.IsZeroTerminated => IsZeroTerminated;
    int? GirModel.ArrayType.FixedSize => FixedSize;
    bool GirModel.ArrayType.IsPointer => AnyTypeReference.CTypeReference?.IsPointer ?? false;
    bool GirModel.ArrayType.IsConst => AnyTypeReference.CTypeReference?.IsConst ?? false;
    bool GirModel.ArrayType.IsVolatile => AnyTypeReference.CTypeReference?.IsVolatile ?? false;
    GirModel.AnyType GirModel.ArrayType.AnyType => AnyTypeReference.Match(
        typeReference => GirModel.AnyType.From(typeReference.GetResolvedType()),
        arrayTypeReference => GirModel.AnyType.From(arrayTypeReference)
    );
}
