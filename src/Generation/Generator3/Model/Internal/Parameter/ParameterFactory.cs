using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model.Internal
{
    public static class ParameterFactory
    {
        public static IEnumerable<Parameter> CreateInternalModels(this IEnumerable<GirModel.Parameter> parameters)
            => parameters.Select(CreateInternalModel);

        private static Parameter CreateInternalModel(this GirModel.Parameter parameter) => parameter.AnyType.Match(
            type => type switch
            {
                GirModel.String => new StringParameter(parameter),
                GirModel.Pointer => new PointerParameter(parameter),
                GirModel.UnsignedPointer => new UnsignedPointerParameter(parameter),
                GirModel.Class => new ClassParameter(parameter),
                GirModel.Interface => new InterfaceParameter(parameter),
                GirModel.Union => new UnionParameter(parameter),
                GirModel.Record => new SafeHandleRecordParameter(parameter),
                GirModel.PrimitiveValueType => new PrimitiveValueTypeParameter(parameter),
                GirModel.Callback => new CallbackParameter(parameter),
                GirModel.Enumeration => new EnumerationParameter(parameter),
                GirModel.Bitfield => new BitfieldParameter(parameter),
                GirModel.Void => new VoidParameter(parameter),
                
                _ => throw new Exception($"Parameter \"{parameter.Name}\" of type {parameter.AnyType} can not be converted into a model")
            },
            arrayType => arrayType.AnyType.Match<Parameter>(
                type => type switch
                {
                    GirModel.Class => new ArrayClassParameter(parameter),
                    GirModel.Interface => new ArrayInterfaceParameter(parameter),
                    GirModel.Record when arrayType.IsPointer => new ArrayPointerRecordParameter(parameter),
                    GirModel.Record => new ArrayRecordParameter(parameter),
                    GirModel.String => new ArrayStringParameter(parameter),
                    GirModel.Enumeration => new ArrayEnumerationParameter(parameter),
                    _ => new StandardParameter(parameter)
                },
                _ => throw new NotSupportedException("Arrays of arrays not yet supported")
            )
        );
    }
}
