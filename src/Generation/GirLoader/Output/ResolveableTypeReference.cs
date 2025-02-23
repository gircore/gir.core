namespace GirLoader.Output;

public class ResolveableTypeReference : TypeReference
{
    private Type? _resolvedType;
    public override Type? Type => _resolvedType;

    public ResolveableTypeReference(SymbolNameReference? symbolNameReference, CTypeReference? ctype)
        : base(symbolNameReference, ctype)
    {
    }

    public void ResolveAs(Type type)
    {
        _resolvedType = type;
    }
}
