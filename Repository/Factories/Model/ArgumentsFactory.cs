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

        public IEnumerable<Argument> Create(ParametersInfo? parameters, bool throws = false)
        {
            var list = new List<Argument>();

            if (parameters is { })
            {
                if (parameters.InstanceParameter is { })
                    list.Add(_argumentFactory.Create(parameters.InstanceParameter));

                list.AddRange(parameters.Parameters.Select(arg => _argumentFactory.Create(arg)));

                if (throws)
                {
                    list.Add(_argumentFactory.Create(
                        name: "error",
                        type: "GError",
                        direction: Direction.OutCalleeAllocates,
                        transfer: Transfer.Full,
                        nullable: false,
                        isPointer: true
                    ));
                }
            }

            return list;
        }
    }
}
