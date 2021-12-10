using System.Collections.Generic;
using GirLoader.Helper;

namespace GirLoader.Output
{
    public partial class Function : Symbol
    {
        private readonly Repository _repository;
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }
        public string Name { get; }
        public Function(Repository repository, string name, SymbolName originalName, ReturnValue returnValue, ParameterList parameterList) : base(originalName)
        {
            _repository = repository;
            ReturnValue = returnValue;
            ParameterList = parameterList;
            Name = name;
        }

        internal override IEnumerable<TypeReference> GetTypeReferences()
        {
            return IEnumerables.Concat(
                ReturnValue.GetTypeReferences(),
                ParameterList.GetSymbolReferences()
            );
        }

        internal override bool GetIsResolved()
            => ReturnValue.GetIsResolved() && ParameterList.GetIsResolved();

        public override string ToString()
            => OriginalName;
    }
}
