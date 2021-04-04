using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Method : Element
    {
        public ReturnValue ReturnValue { get; }
        public IEnumerable<Argument> Arguments { get; }
        public Argument? InstanceArgument { get; }

        public Method(ElementName elementName, ElementManagedName elementManagedName, ReturnValue returnValue, IEnumerable<Argument> arguments, Argument? instanceArg = null) : base(elementName, elementManagedName)
        {
            ReturnValue = returnValue;
            Arguments = arguments;
            InstanceArgument = instanceArg;
        }

        public override IEnumerable<SymbolReference> GetSymbolReferences()
        {
            return IEnumerables.Concat(
                ReturnValue.GetSymbolReferences(),
                Arguments.SelectMany(x => x.GetSymbolReferences()),
                InstanceArgument?.GetSymbolReferences() ?? Enumerable.Empty<SymbolReference>());
        }

        public override bool GetIsResolved()
            => ReturnValue.GetIsResolved() && Arguments.AllResolved();
    }
}
