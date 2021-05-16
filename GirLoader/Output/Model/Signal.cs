using System.Collections.Generic;
using Gir.Helper;

namespace Gir.Output.Model
{
    public class Signal : Element
    {
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }

        public Signal(ElementName elementName, SymbolName symbolName, ReturnValue returnValue, ParameterList parameterList) : base(elementName, symbolName)
        {
            ReturnValue = returnValue;
            ParameterList = parameterList;
        }

        public override IEnumerable<TypeReference> GetTypeReferences()
        {
            return IEnumerables.Concat(
                ReturnValue.GetTypeReferences(),
                ParameterList.GetSymbolReferences()
            );
        }

        public override bool GetIsResolved()
            => ReturnValue.GetIsResolved() && ParameterList.GetIsResolved();
    }
}
