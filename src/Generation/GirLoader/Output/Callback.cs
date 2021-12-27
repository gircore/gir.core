namespace GirLoader.Output
{
    public partial class Callback : ComplexType
    {
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }
        public bool Introspectable { get; }

        public Callback(Repository repository, string? ctype, string name, ReturnValue returnValue, ParameterList parameterList, bool introspectable) : base(repository, ctype, name)
        {
            ReturnValue = returnValue;
            ParameterList = parameterList;
            Introspectable = introspectable;
        }

        internal override bool Matches(TypeReference typeReference)
        {
            if (typeReference.CTypeReference is null)
                return false;

            return typeReference.CTypeReference.CType == CType;
        }
    }
}
