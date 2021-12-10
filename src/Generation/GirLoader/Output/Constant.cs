using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Constant : Symbol
    {
        private readonly Repository _repository;
        public string Value { get; }
        public TypeReference TypeReference { get; }

        public Constant(Repository repository, string originalName, TypeReference typeReference, string value) : base(originalName)
        {
            _repository = repository;
            TypeReference = typeReference;
            Value = value;
        }

        internal override IEnumerable<TypeReference> GetTypeReferences()
        {
            yield return TypeReference;
        }

        internal override bool GetIsResolved()
            => TypeReference.GetIsResolved();
    }
}
