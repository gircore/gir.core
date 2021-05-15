using System.Collections.Generic;
using System.Linq;

namespace Gir.Model
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

        public ParameterList Create(Xml.Parameters? parameters, NamespaceName namespaceName, bool throws = false)
        {
            List<SingleParameter> list = new();
            InstanceParameter? instanceParameter = null;

            if (parameters is { })
            {
                if (parameters.InstanceParameter is { })
                    instanceParameter = _instanceParameterFactory.Create(parameters.InstanceParameter, namespaceName);

                list = parameters.List.Select(arg => _singleParameterFactory.Create(arg, namespaceName)).ToList();

                if (throws)
                {
                    var parameterInfo = new Xml.Parameter()
                    {
                        Name = "error",
                        TransferOwnership = "full",
                        Direction = "out",
                        CallerAllocates = false,
                        Type = new Xml.Type()
                        {
                            Name = "GLib.Error",
                            CType = "GError*"
                        }
                    };

                    list.Add(_singleParameterFactory.Create(parameterInfo, namespaceName));
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
