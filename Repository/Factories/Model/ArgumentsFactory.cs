using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IArgumentsFactory
    {
        IEnumerable<Argument> Create(ParametersInfo? parameters);
    }

    public class ArgumentsFactory : IArgumentsFactory
    {
        private readonly IArgumentFactory _argumentFactory;

        public ArgumentsFactory(IArgumentFactory argumentFactory)
        {
            _argumentFactory = argumentFactory;
        }

        public IEnumerable<Argument> Create(ParametersInfo? parameters)
        {
            var list = new List<Argument>();

            if (parameters is { })
            {
                if (parameters.InstanceParameter is { })
                    list.Add(_argumentFactory.Create(parameters.InstanceParameter));

                list.AddRange(parameters.Parameters.Select(arg => _argumentFactory.Create(arg)));
            }

            return list;
        }
    }
}
