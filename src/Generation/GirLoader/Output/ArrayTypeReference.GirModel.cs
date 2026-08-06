namespace GirLoader.Output;

public partial class ArrayTypeReference : GirModel.ArrayTypeReference
{
    int? GirModel.ArrayTypeReference.Length => Length;
    bool GirModel.ArrayTypeReference.IsZeroTerminated => IsZeroTerminated;
    int? GirModel.ArrayTypeReference.FixedSize => FixedSize;
    bool GirModel.ArrayTypeReference.IsPointer => AnyTypeReference.CTypeReference?.IsPointer ?? false;
    bool GirModel.ArrayTypeReference.IsConst => AnyTypeReference.CTypeReference?.IsConst ?? false;
    bool GirModel.ArrayTypeReference.IsVolatile => AnyTypeReference.CTypeReference?.IsVolatile ?? false;
    GirModel.AnyTypeReference GirModel.ArrayTypeReference.AnyTypeReference => AnyTypeReference.Match(GirModel.AnyTypeReference.From, GirModel.AnyTypeReference.From);
}
