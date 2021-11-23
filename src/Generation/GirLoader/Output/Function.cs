using System.Collections.Generic;
using GirLoader.Helper;

namespace GirLoader.Output
{
    public partial class Function : Symbol
    {
        private readonly Repository _repository;
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }

        public Function(Repository repository, SymbolName originalName, SymbolName symbolName, ReturnValue returnValue, ParameterList parameterList) : base(originalName, symbolName)
        {
            _repository = repository;
            ReturnValue = returnValue;
            ParameterList = parameterList;
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
