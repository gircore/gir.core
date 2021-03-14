using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;
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
                    list.Add(_argumentFactory.Create(
                        name: "error",
                        type: "GLib.Error",
                        ctype: "GError",
                        direction: Direction.OutCalleeAllocates,
                        transfer: Transfer.Full,
                        nullable: false,
                        currentNamespace: currentNamespace
                    ));
                }
            }

            return list;
        }
    }
}
