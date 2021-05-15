using System.Collections.Generic;

namespace Gir.Model
{
    public class Callback : Type
    {
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }

        public Callback(Repository repository, CTypeName? ctypeName, TypeName typeName, SymbolName symbolName, ReturnValue returnValue, ParameterList parameterList) : base(repository, ctypeName, typeName, symbolName)
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
