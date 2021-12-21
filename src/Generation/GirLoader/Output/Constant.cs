namespace GirLoader.Output
{
    public partial class Constant
    {
        private readonly Repository _repository;

        public string Name { get; }
        public string Value { get; }
        public TypeReference TypeReference { get; }

        public Constant(Repository repository, string name, TypeReference typeReference, string value)
        {
            _repository = repository;
            Name = name;
            TypeReference = typeReference;
            Value = value;
        }
    }
}
