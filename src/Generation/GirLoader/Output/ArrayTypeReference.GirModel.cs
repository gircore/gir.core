namespace GirLoader.Output;

public partial class ArrayTypeReference : GirModel.ArrayType
{
    int? GirModel.ArrayType.Length => Length;
    bool GirModel.ArrayType.IsZeroTerminated => IsZeroTerminated;
    int? GirModel.ArrayType.FixedSize => FixedSize;
    bool GirModel.ArrayType.IsPointer => ElementTypeReference.CTypeReference?.IsPointer ?? false;
    bool GirModel.ArrayType.IsConst => ElementTypeReference.CTypeReference?.IsConst ?? false;
    bool GirModel.ArrayType.IsVolatile => ElementTypeReference.CTypeReference?.IsVolatile ?? false;
    GirModel.AnyType GirModel.ArrayType.AnyType => ElementTypeReference.GetResolvedAnyType();
}
