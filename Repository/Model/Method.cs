using System.Collections.Generic;

namespace Repository.Model
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

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            return IEnumerables.Concat(
                ReturnValue.GetSymbolReferences(),
                ParameterList.GetSymbolReferences()
            );
        }

        public override bool GetIsResolved()
            => ReturnValue.GetIsResolved() && ParameterList.GetIsResolved();

        public override string ToString()
            => Name;
    }
}
