using System.Collections.Generic;
using GirLoader.Helper;

namespace GirLoader.Output
{
    public partial class Method : Symbol
    {
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }
        public string Name { get; }

        public Method(string originalName, string name, ReturnValue returnValue, ParameterList parameterList) : base(originalName)
        {
            ReturnValue = returnValue;
            ParameterList = parameterList;
            Name = name;
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
