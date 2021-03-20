using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Signal : Element
    {
        public ReturnValue ReturnValue { get; }
        public IEnumerable<Argument> Arguments { get; }

        public Signal(ElementName elementName, SymbolName symbolName, ReturnValue returnValue, IEnumerable<Argument> arguments) : base(elementName, symbolName)
        {
            ReturnValue = returnValue;
            Arguments = arguments;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            return IEnumerables.Concat(
                ReturnValue.GetSymbolReferences(),
                Arguments.SelectMany(x => x.GetSymbolReferences())
            );
        }

        public override bool GetIsResolved()
            => ReturnValue.GetIsResolved() && Arguments.AllResolved();
    }
}
