namespace GirLoader.Output
{
    public partial class Constructor
    {
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }
        public string Name { get; }
        public string Identifier { get; }

        public Constructor(string name, string identifier, ReturnValue returnValue, ParameterList parameterList)
        {
            Name = name;
            Identifier = identifier;
            ReturnValue = returnValue;
            ParameterList = parameterList;
        }

        public override string ToString()
            => Identifier;
    }
}
