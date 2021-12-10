using System.Collections.Generic;
using GirLoader.Helper;

namespace GirLoader.Output
{
    public partial class Constructor : Symbol
    {
        public ReturnValue ReturnValue { get; }
        public ParameterList ParameterList { get; }
        public string Name { get; }
        public Constructor(string name, string originalName, ReturnValue returnValue, ParameterList parameterList) : base(originalName)
        {
            Name = name;
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

        public override string ToString()
            => OriginalName;
    }
}
