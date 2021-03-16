using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    internal class ArgumentsFactory
    {
        private readonly ArgumentFactory _argumentFactory;

        public ArgumentsFactory(ArgumentFactory argumentFactory)
        {
            _argumentFactory = argumentFactory;
        }

        public IEnumerable<Argument> Create(ParametersInfo? parameters, NamespaceName currentNamespace, bool throws = false)
        {
            var list = new List<Argument>();

            if (parameters is { })
            {
                if (parameters.InstanceParameter is { })
                    list.Add(_argumentFactory.Create(parameters.InstanceParameter, currentNamespace));

                list.AddRange(parameters.Parameters.Select(arg => _argumentFactory.Create(arg, currentNamespace)));

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
                    
                    list.Add(_argumentFactory.Create(parameterInfo, currentNamespace));
                }
            }

            return list;
        }
    }
}
