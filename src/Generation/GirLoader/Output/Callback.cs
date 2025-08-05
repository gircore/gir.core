namespace GirLoader.Output;

public partial class Callback : ComplexType
{
    private ComplexType? _parent;

    public ReturnValue ReturnValue { get; }
    public ParameterList ParameterList { get; }
    public bool Introspectable { get; }
    public bool Throws { get; }

    public Callback(Repository repository, string? ctype, string name, ReturnValue returnValue, ParameterList parameterList, bool introspectable, bool throws) : base(repository, ctype, name)
    {
        ReturnValue = returnValue;
        ParameterList = parameterList;
        Introspectable = introspectable;
        Throws = throws;
    }

    internal void SetParent(ComplexType parent)
    {
        this._parent = parent;
    }

    internal override bool Matches(TypeReference typeReference)
    {
        if (typeReference.CTypeReference is null)
            return false;

        return typeReference.CTypeReference.CType == CType;
    }
}
