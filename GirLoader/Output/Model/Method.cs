using System.Collections.Generic;
using GirLoader.Helper;

namespace GirLoader.Output.Model
{
    public class Method : Element
    {
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }

        public Method(ElementName elementName, SymbolName symbolName, ReturnValue returnValue, ParameterList parameterList) : base(elementName, symbolName)
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

        public override string ToString()
            => Name;
    }
}
