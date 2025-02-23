namespace GirModel;

public interface ArrayType
{
    int? Length { get; }
    bool IsZeroTerminated { get; }
    int? FixedSize { get; }
    bool IsPointer { get; }
    bool IsConst { get; }
    bool IsVolatile { get; }
    AnyType AnyType { get; }
}
