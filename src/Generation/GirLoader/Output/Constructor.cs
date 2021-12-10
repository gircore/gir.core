namespace GirLoader.Output
{
    public partial class Constructor : Symbol
    {
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }
        public string Name { get; }
        public Constructor(string name, string originalName, ReturnValue returnValue, ParameterList parameterList) : base(originalName)
        {
            Name = name;
            ReturnValue = returnValue;
            ParameterList = parameterList;
        }

        public override string ToString()
            => OriginalName;
    }
}
