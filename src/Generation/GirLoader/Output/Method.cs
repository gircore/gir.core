namespace GirLoader.Output;

public partial class Method : Callable
{
    private ComplexType? _parent;

    public string Identifier { get; }
    public ReturnValue ReturnValue { get; }
    public ParameterList ParameterList { get; }
    public string Name { get; }
    public bool Throws { get; }
    public bool Introspectable { get; }
    public PropertyReference? GetProperty { get; }
    public PropertyReference? SetProperty { get; }
    public string? Version { get; }
    public ShadowsReference? ShadowsReference { get; }
    public ShadowedByReference? ShadowedByReference { get; }

    public Method(string identifier, string name, ReturnValue returnValue, ParameterList parameterList, bool throws, bool introspectable, PropertyReference? getProperty, PropertyReference? setProperty, string? version, ShadowsReference? shadows, ShadowedByReference? shadowedBy)
    {
        Identifier = identifier;
        ReturnValue = returnValue;
        ParameterList = parameterList;
        Name = name;
        Throws = throws;
        Introspectable = introspectable;
        GetProperty = getProperty;
        SetProperty = setProperty;
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
