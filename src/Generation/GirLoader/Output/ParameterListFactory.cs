using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output;

internal class ParameterListFactory
{
    private readonly SingleParameterFactory _singleParameterFactory;
    private readonly InstanceParameterFactory _instanceParameterFactory;

    public ParameterListFactory(SingleParameterFactory singleParameterFactory, InstanceParameterFactory instanceParameterFactory)
    {
        _singleParameterFactory = singleParameterFactory;
        _instanceParameterFactory = instanceParameterFactory;
    }

    public ParameterList Create(Input.Parameters? parameters)
    {
        List<SingleParameter> list = new();
        InstanceParameter? instanceParameter = null;

        if (parameters is { })
        {
            if (parameters.InstanceParameter is { })
                instanceParameter = _instanceParameterFactory.Create(parameters.InstanceParameter);

            list = parameters.List.Select(arg => _singleParameterFactory.Create(arg)).ToList();
        }

        return new ParameterList()
        {
            InstanceParameter = instanceParameter,
            SingleParameters = list
        };
    }
}
