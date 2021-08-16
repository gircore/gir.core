using System.Collections.Generic;
using System.Linq;
using GirLoader.Helper;

namespace GirLoader.Output
{
    public class ParameterList
    {
        public InstanceParameter? InstanceParameter { get; init; }

        public IEnumerable<SingleParameter> SingleParameters { get; init; } = Enumerable.Empty<SingleParameter>();

        public IEnumerable<TypeReference> GetSymbolReferences()
        {
            var ret = SingleParameters.SelectMany(x => x.GetTypeReferences());

            if (InstanceParameter is { })
                ret = ret.Append(InstanceParameter.TypeReference);

            return ret;
        }

        public bool GetIsResolved()
        {
            var instanceParameterResolved = InstanceParameter is null || InstanceParameter.GetIsResolved();
            return SingleParameters.All(x => x.GetIsResolved()) && instanceParameterResolved;
        }

        public bool Any()
        {
            return InstanceParameter is { } || SingleParameters.Any();
        }

        public IEnumerable<Parameter> GetParameters()
        {
            IEnumerable<Parameter> ret = SingleParameters;

            if (InstanceParameter is { })
                ret = ret.Prepend(InstanceParameter); //Prepend to keep order

            return ret;
        }
    }
}
