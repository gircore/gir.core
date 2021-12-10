namespace GirLoader.Output
{
    public partial class Method : Symbol
    {
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }
        public string Name { get; }

        public Method(string originalName, string name, ReturnValue returnValue, ParameterList parameterList) : base(originalName)
        {
            ReturnValue = returnValue;
            ParameterList = parameterList;
            Name = name;
        }

        public override string ToString()
            => OriginalName;
    }
}
