using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Repository.Xml;

#nullable enable

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
            if (parameters is null)
                return Enumerable.Empty<Argument>();

            return parameters.Parameters.Select(arg => _argumentFactory.Create(arg));
        }
    }
}
