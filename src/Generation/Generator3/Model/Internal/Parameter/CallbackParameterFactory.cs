using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model.Internal
{
    public static class CallbackParameterFactory
    {
        public static IEnumerable<Parameter> CreateInternalModelsForCallback(this IEnumerable<GirModel.Parameter> parameters)
            => parameters.Select(CreateInternalModelForCallback);

        private static Parameter CreateInternalModelForCallback(this GirModel.Parameter parameter) => parameter.AnyType.Match<Parameter>(
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
                GirModel.Enumeration => new StandardParameter(parameter),
                GirModel.Bitfield => new StandardParameter(parameter),
                GirModel.Void => new VoidParameter(parameter),
                
                _ => throw new Exception($"Parameter \"{parameter.Name}\" of type {parameter.AnyType} can not be converted into a model")
            },
            arrayType => arrayType.Type switch
            {
                GirModel.Record => new ArrayPointerRecordParameter(parameter),
                GirModel.String => new ArrayStringParameter(parameter),
                _ => new StandardParameter(parameter)   
            }
        );
    }
}
