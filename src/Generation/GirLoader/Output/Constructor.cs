namespace GirLoader.Output;

public partial class Constructor : Callable
{
    private ComplexType? _parent;

    public ReturnValue ReturnValue { get; }
    public ParameterList ParameterList { get; }
    public string Name { get; }
    public string Identifier { get; }
    public bool Throws { get; }
    public string? Version { get; }
    public ShadowsReference? ShadowsReference { get; }
    public ShadowedByReference? ShadowedByReference { get; }

    public Constructor(string name, string identifier, ReturnValue returnValue, ParameterList parameterList, bool throws, string? version, ShadowsReference? shadows, ShadowedByReference? shadowedBy)
    {
        Name = name;
        Identifier = identifier;
        ReturnValue = returnValue;
        ParameterList = parameterList;
        Throws = throws;
        Version = version;
        ShadowsReference = shadows;
        ShadowedByReference = shadowedBy;
    }

    internal void SetParent(ComplexType parent)
    {
        this._parent = parent;
    }

    public override string ToString()
        => Identifier;
}
