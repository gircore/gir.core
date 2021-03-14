using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;

namespace Repository.Model
{
    public class Callback : Symbol
    {
        public ReturnValue ReturnValue { get; }
        public IEnumerable<Argument> Arguments { get; }

        public Callback(Namespace @namespace, CTypeName? ctypeName, TypeName typeName, NativeName nativeName, ManagedName managedName, ReturnValue returnValue, IEnumerable<Argument> arguments) : base(@namespace, ctypeName, typeName, nativeName, managedName)
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
