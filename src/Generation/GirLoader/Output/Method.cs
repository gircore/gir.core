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
        public string? Version { get; }

        public Method(string identifier, string name, ReturnValue returnValue, ParameterList parameterList, bool introspectable, PropertyReference? getProperty, PropertyReference? setProperty, string? version)
        {
            Identifier = identifier;
            ReturnValue = returnValue;
            ParameterList = parameterList;
            Name = name;
            Introspectable = introspectable;
            GetProperty = getProperty;
            SetProperty = setProperty;
            Version = version;
        }

        public override string ToString()
            => Identifier;
    }
}
