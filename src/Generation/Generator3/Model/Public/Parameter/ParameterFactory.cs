using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model.Public
{
    public static class ParameterFactory
    {
        public static IEnumerable<Parameter> CreatePublicModels(this IEnumerable<GirModel.Parameter> parameters)
            => parameters.Select(CreatePublicModel);

        private static Parameter CreatePublicModel(this GirModel.Parameter parameter) => parameter.AnyTypeReference.AnyType.Match<Parameter>(
            type => type switch
            {
                GirModel.Class => new ClassParameter(parameter),
                GirModel.Bitfield => new BitfieldParameter(parameter),
                GirModel.Record => new RecordParameter(parameter),
                _ => new StandardParameter(parameter) //TODO: Remove Standard Parameter
            },
            _ => new StandardParameter(parameter) //TODO: Remove Standard Parameter
        );
    }
}
