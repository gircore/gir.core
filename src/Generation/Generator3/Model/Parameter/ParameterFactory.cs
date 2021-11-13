using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model
{
    public static class ParameterFactory
    {
        public static IEnumerable<Parameter> CreatePublicModel(this IEnumerable<GirModel.Parameter> parameters)
            => parameters.Select(CreatePublicModel);

        private static Parameter CreatePublicModel(this GirModel.Parameter parameter) => parameter.AnyType.Match<Parameter>(
            type => new Public.StandardParameter(parameter),
            arrayType => new Public.StandardParameter(parameter)
        );
    }
}
