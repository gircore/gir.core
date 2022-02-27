namespace GirLoader.Output
{
    public partial class Method
    {
        public string Identifier { get; }
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }
        public string Name { get; }
        public bool Introspectable { get; }
        public PropertyReference? GetProperty { get; }
        public PropertyReference? SetProperty { get; }

        public Method(string identifier, string name, ReturnValue returnValue, ParameterList parameterList, bool introspectable, PropertyReference? getProperty, PropertyReference? setProperty)
        {
            Identifier = identifier;
            ReturnValue = returnValue;
            ParameterList = parameterList;
            Name = name;
            Introspectable = introspectable;
            GetProperty = getProperty;
            SetProperty = setProperty;
        }

        public override string ToString()
            => Identifier;
    }
}
