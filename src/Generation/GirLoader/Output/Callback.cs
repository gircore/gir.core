using System.Collections.Generic;
using GirLoader.Helper;

namespace GirLoader.Output
{
    public partial class Callback : ComplexType
    {
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }

        public Callback(Repository repository, CType? ctype, TypeName originalName, ReturnValue returnValue, ParameterList parameterList) : base(repository, ctype, originalName)
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

        internal override bool Matches(TypeReference typeReference)
        {
            if (typeReference.CTypeReference is null)
                return false;

            return typeReference.CTypeReference.CType == CType;
        }
    }
}
