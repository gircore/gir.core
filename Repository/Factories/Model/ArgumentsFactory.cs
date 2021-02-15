using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IArgumentsFactory
    {
        IEnumerable<Argument> Create(ParametersInfo? parameters, bool throws = false);
    }

    public class ArgumentsFactory : IArgumentsFactory
    {
        private readonly IArgumentFactory _argumentFactory;

        public ArgumentsFactory(IArgumentFactory argumentFactory)
        {
            _argumentFactory = argumentFactory;
        }

        public IEnumerable<Argument> Create(ParametersInfo? parameters, bool throws)
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
                        type: "GLib.Error",
                        isArray: false,
                        direction: Direction.OutCalleeAllocates,
                        transfer: Transfer.Full,
                        nullable: false
                    ));
                }
            }

            return list;
        }
    }
}
