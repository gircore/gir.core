namespace GirModel;

public interface ReturnType : Nullable, ElementTypeContainer
{
    AnyType AnyType { get; }
    Transfer Transfer { get; }
    bool IsPointer { get; }
}
