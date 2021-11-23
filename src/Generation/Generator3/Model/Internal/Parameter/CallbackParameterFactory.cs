using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model.Internal
{
    public static class CallbackParameterFactory
    {
        public static IEnumerable<Parameter> CreateInternalModelsForCallback(this IEnumerable<GirModel.Parameter> parameters)
            => parameters.Select(CreateInternalModelForCallback);

        private static Parameter CreateInternalModelForCallback(this GirModel.Parameter parameter) => parameter.AnyTypeReference.AnyType.Match(
            type => type switch
            {
                GirModel.String => new StringParameter(parameter),
                GirModel.Pointer => new PointerParameter(parameter),
                GirModel.UnsignedPointer => new UnsignedPointerParameter(parameter),
                GirModel.Class => new ClassParameter(parameter),
                GirModel.Interface => new InterfaceParameter(parameter),
                GirModel.Union => new UnionParameter(parameter),

                //Callbacks do not support record safe handles in parameters
                GirModel.Record => new PointerRecordParameter(parameter),

                GirModel.PrimitiveValueType => new StandardParameter(parameter),
                GirModel.Callback => new CallbackParameter(parameter),
                GirModel.Enumeration => new EnumerationParameter(parameter),
                GirModel.Bitfield => new BitfieldParameter(parameter),
                GirModel.Void => new VoidParameter(parameter),

                _ => throw new Exception($"Parameter \"{parameter.Name}\" of type {parameter.AnyTypeReference.AnyType} can not be converted into a model")
            },
            arrayType => arrayType.AnyTypeReference.AnyType.Match<Parameter>(
                type => type switch
                {
                    GirModel.Class => new ArrayClassParameter(parameter),
                    GirModel.Record when arrayType.AnyTypeReference.IsPointer => new ArrayPointerRecordParameter(parameter),
                    GirModel.Record => new ArrayRecordParameter(parameter),
                    GirModel.String => new ArrayStringParameter(parameter),
                    _ => new StandardParameter(parameter)
                },
                _ => throw new NotSupportedException("Arrays of arrays not yet supported")
            )
        );
    }
}
