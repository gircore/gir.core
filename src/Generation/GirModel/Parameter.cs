namespace GirModel;

public interface Parameter : Nullable
{
    string Name { get; }
    Direction Direction { get; }
    Transfer Transfer { get; }
    bool Optional { get; }
    bool CallerAllocates { get; }
    int? Closure { get; }
    int? Destroy { get; }
    bool IsPointer { get; }
    bool IsConst { get; }
    bool IsVolatile { get; }
    OneOf.OneOf<AnyType, VarArgs> AnyTypeOrVarArgs { get; }
    Scope? Scope { get; }
}
