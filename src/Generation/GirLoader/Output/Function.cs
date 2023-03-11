using System.Diagnostics;

namespace GirLoader.Output;

public partial class Function : Callable
{
    private readonly Repository _repository;
    private ComplexType? _parent;

    public ReturnValue ReturnValue { get; }
    public ParameterList ParameterList { get; }
    public string Name { get; }
    public string Identifier { get; }
    public bool Throws { get; }
    public bool Introspectable { get; }
    public string? Version { get; }
    public ShadowsReference? ShadowsReference { get; }
    public ShadowedByReference? ShadowedByReference { get; }

    public Function(Repository repository, string name, string identifier, ReturnValue returnValue, ParameterList parameterList, bool throws, bool introspectable, string? version, ShadowsReference? shadows, ShadowedByReference? shadowedBy)
    {
        _repository = repository;
        ReturnValue = returnValue;
        ParameterList = parameterList;
        Name = name;
        Identifier = identifier;
        Throws = throws;
        Introspectable = introspectable;
        Version = version;
        ShadowsReference = shadows;
        ShadowedByReference = shadowedBy;
    }

    internal void SetParent(ComplexType parent)
    {
        Debug.Assert(
            condition: parent.Repository == _repository,
            message: $"Parent of function {Name} must be in {_repository} but is in {parent.Name} / {parent.Repository}"
        );

        this._parent = parent;
    }

    public override string ToString()
        => Identifier;
}
