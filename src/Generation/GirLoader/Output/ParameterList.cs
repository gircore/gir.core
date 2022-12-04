using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output;

public class ParameterList
{
    public InstanceParameter? InstanceParameter { get; init; }

    public IEnumerable<SingleParameter> SingleParameters { get; init; } = Enumerable.Empty<SingleParameter>();

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
