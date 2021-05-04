using System.Collections.Generic;
using System.Linq;
using Repository.Xml;

namespace Repository.Model
{
    internal class ParameterListFactory
    {
        private readonly SingleParameterFactory _singleParameterFactory;
        private readonly InstanceParameterFactory _instanceParameterFactory;

        public ParameterListFactory(SingleParameterFactory singleParameterFactory, InstanceParameterFactory instanceParameterFactory)
        {
            _singleParameterFactory = singleParameterFactory;
            _instanceParameterFactory = instanceParameterFactory;
        }

        public ParameterList Create(ParametersInfo? parameters, NamespaceName currentNamespace, bool throws = false)
        {
            List<SingleParameter> list = new();
            InstanceParameter? instanceParameter = null;

            if (parameters is { })
            {
                if (parameters.InstanceParameter is { })
                    instanceParameter = _instanceParameterFactory.Create(parameters.InstanceParameter, currentNamespace);

                list = parameters.Parameters.Select(arg => _singleParameterFactory.Create(arg, currentNamespace)).ToList();

                if (throws)
                {
                    var parameterInfo = new ParameterInfo()
                    {
                        Name = "error",
                        TransferOwnership = "full",
                        Direction = "out",
                        CallerAllocates = false,
                        Type = new TypeInfo()
                        {
                            Name = "GLib.Error",
                            CType = "GError*"
                        }
                    };

                    list.Add(_singleParameterFactory.Create(parameterInfo, currentNamespace));
                }
            }

            return new ParameterList()
            {
                InstanceParameter = instanceParameter,
                SingleParameters = list
            };
        }
    }
}
