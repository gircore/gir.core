namespace GirLoader.Output;

public partial class Signal
{
    public string Name { get; }
    public ReturnValue ReturnValue { get; }
    public ParameterList ParameterList { get; }
    public bool Introspectable { get; }

    public Signal(string name, ReturnValue returnValue, ParameterList parameterList, bool introspectable)
    {
        Name = name;
        ReturnValue = returnValue;
        ParameterList = parameterList;
        Introspectable = introspectable;
    }
}
