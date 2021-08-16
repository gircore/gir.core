using System.Collections.Generic;
using GirLoader.Helper;

namespace GirLoader.Output
{
    public class Signal : Symbol
    {
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }

        public Signal(SymbolName originalName, SymbolName symbolName, ReturnValue returnValue, ParameterList parameterList) : base(originalName, symbolName)
        {
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
    }
}
