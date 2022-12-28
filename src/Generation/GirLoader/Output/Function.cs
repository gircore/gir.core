using System.Diagnostics;

namespace GirLoader.Output;

public partial class Function
{
    private readonly Repository _repository;
    private ComplexType? _parent;

    public ReturnValue ReturnValue { get; }
    public ParameterList ParameterList { get; }
    public string Name { get; }
    public string Identifier { get; }
    public bool Introspectable { get; }
    public string? Version { get; }

    public Function(Repository repository, string name, string identifier, ReturnValue returnValue, ParameterList parameterList, bool introspectable, string? version)
    {
        _repository = repository;
        ReturnValue = returnValue;
        ParameterList = parameterList;
        Name = name;
        Identifier = identifier;
        Introspectable = introspectable;
        Version = version;
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
