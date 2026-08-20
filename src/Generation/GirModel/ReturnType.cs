namespace GirModel;

public interface ReturnType : Nullable
{
    AnyTypeReference AnyTypeReference { get; }
    Transfer Transfer { get; }
    bool IsPointer { get; }
}
