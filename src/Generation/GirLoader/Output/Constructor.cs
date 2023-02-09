namespace GirLoader.Output;

public partial class Constructor
{
    public ReturnValue ReturnValue { get; }
    public ParameterList ParameterList { get; }
    public string Name { get; }
    public string Identifier { get; }
    public bool Throws { get; }
    public string? Version { get; }

    public Constructor(string name, string identifier, ReturnValue returnValue, ParameterList parameterList, bool throws, string? version)
    {
        Name = name;
        Identifier = identifier;
        ReturnValue = returnValue;
        ParameterList = parameterList;
        Throws = throws;
        Version = version;
    }

    public override string ToString()
        => Identifier;
}
