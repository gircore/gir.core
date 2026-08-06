namespace GirLoader.Output;

public partial class SingleParameter : Parameter
{
    public OneOf.OneOf<AnyTypeReference, VarArgs> AnyTypeReferenceOrVarArgs { get; }
    public Direction Direction { get; }
    public Transfer Transfer { get; }
    public bool Nullable { get; }
    public bool Optional { get; }
    public bool CallerAllocates { get; }
    public int? ClosureIndex { get; }
    public int? DestroyIndex { get; }
    public Scope? CallbackScope { get; }
    public string Name { get; }

    public SingleParameter(string name, OneOf.OneOf<AnyTypeReference, VarArgs> anyTypeReferenceOrVarArgs, Direction direction, Transfer transfer, bool nullable, bool optional, bool callerAllocates, int? closureIndex, int? destroyIndex, Scope? scope)
    {
        Name = name;
        AnyTypeReferenceOrVarArgs = anyTypeReferenceOrVarArgs;
        Direction = direction;
        Transfer = transfer;
        Nullable = nullable;
        Optional = optional;
        CallerAllocates = callerAllocates;
        ClosureIndex = closureIndex;
        DestroyIndex = destroyIndex;
        CallbackScope = scope;
    }

    public override string ToString()
    {
        return $"Parameter {Name}";
    }
}
