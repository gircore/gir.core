namespace GirLoader.Output
{
    public partial class Method
    {
        public string Identifier { get; }
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }
        public string Name { get; }

        public Method(string identifier, string name, ReturnValue returnValue, ParameterList parameterList)
        {
            Identifier = identifier;
            ReturnValue = returnValue;
            ParameterList = parameterList;
            Name = name;
        }

        public override string ToString()
            => Identifier;
    }
}
