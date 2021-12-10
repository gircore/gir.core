namespace GirLoader.Output
{
    public partial class Function : Symbol
    {
        private readonly Repository _repository;
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }
        public string Name { get; }
        public Function(Repository repository, string name, string originalName, ReturnValue returnValue, ParameterList parameterList) : base(originalName)
        {
            _repository = repository;
            ReturnValue = returnValue;
            ParameterList = parameterList;
            Name = name;
        }

        public override string ToString()
            => OriginalName;
    }
}
