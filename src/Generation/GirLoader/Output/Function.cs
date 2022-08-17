namespace GirLoader.Output
{
    public partial class Function
    {
        private readonly Repository _repository;
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

        public override string ToString()
            => Identifier;
    }
}
