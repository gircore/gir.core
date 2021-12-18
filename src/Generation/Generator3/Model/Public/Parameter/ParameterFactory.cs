using System;
using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Internal;

namespace Generator3.Model.Public
{
    public static class ParameterFactory
    {
        public static IEnumerable<Parameter> CreatePublicModels(this IEnumerable<GirModel.Parameter> parameters)
            => parameters.Select(CreatePublicModel);

        private static Parameter CreatePublicModel(this GirModel.Parameter parameter) => parameter.AnyType.Match<Parameter>(
            type => type switch
            {
                GirModel.Class => new ClassParameter(parameter),
                GirModel.Interface => new InterfaceParameter(parameter),
                GirModel.Bitfield => new BitfieldParameter(parameter),
                GirModel.Enumeration => new EnumerationParameter(parameter),
                GirModel.Union => new UnionParameter(parameter),
                GirModel.Record => new RecordParameter(parameter),
                GirModel.Void => new VoidParameter(parameter),
                _ => new StandardParameter(parameter) //TODO: Remove Standard Parameter
            },
            arraytype => arraytype.AnyType.Match<Parameter>(
                type => type switch
                {
                    GirModel.Record => new ArrayRecordParameter(parameter),
                    GirModel.Class => new ArrayClassParameter(parameter),
                    _ => new StandardParameter(parameter)
                },
                _ => throw new NotSupportedException("Arrays of arrays not yet supported")
            )
        );
    }
}
