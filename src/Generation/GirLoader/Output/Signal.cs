namespace GirLoader.Output
{
    public partial class Signal : Symbol
    {
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }

        public Signal(string originalName, ReturnValue returnValue, ParameterList parameterList) : base(originalName)
        {
            ReturnValue = returnValue;
            ParameterList = parameterList;
        }
    }
}
