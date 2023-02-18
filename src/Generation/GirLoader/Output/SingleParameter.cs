namespace GirLoader.Output;

public partial class SingleParameter : Parameter
{
    public OneOf.OneOf<TypeReference, VarArgs> TypeReferenceOrVarArgs { get; }
    public Direction Direction { get; }
    public Transfer Transfer { get; }
    public bool Nullable { get; }
    public bool Optional { get; }
    public bool CallerAllocates { get; }
    public int? ClosureIndex { get; }
    public int? DestroyIndex { get; }
    public Scope? CallbackScope { get; }
    public string Name { get; }

    public SingleParameter(string name, OneOf.OneOf<TypeReference, VarArgs> typeReferenceOrVarArgs, Direction direction, Transfer transfer, bool nullable, bool optional, bool callerAllocates, int? closureIndex, int? destroyIndex, Scope? scope)
    {
        Name = name;
        TypeReferenceOrVarArgs = typeReferenceOrVarArgs;
        Direction = direction;
        Transfer = transfer;
        Nullable = nullable;
        Optional = optional;
        CallerAllocates = callerAllocates;
        ClosureIndex = closureIndex;
        DestroyIndex = destroyIndex;
        CallbackScope = scope;
    }
}
