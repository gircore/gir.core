namespace GirModel;

public interface ArrayTypeReference
{
    int? Length { get; }
    bool IsZeroTerminated { get; }
    int? FixedSize { get; }
    bool IsPointer { get; }
    bool IsConst { get; }
    bool IsVolatile { get; }
    AnyTypeReference AnyTypeReference { get; }
}
