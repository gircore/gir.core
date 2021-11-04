using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model
{
    public static class NativeParameterFactory
    {
        public static IEnumerable<Parameter> CreateNativeModelsForCallbacks(this IEnumerable<GirModel.Parameter> parameters)
            => parameters.Select(CreateNativeModelForCallback);

        private static Parameter CreateNativeModelForCallback(this GirModel.Parameter parameter) => parameter.AnyType.Match<Parameter>(
            type => type switch
            {
                GirModel.String => new Native.StringParameter(parameter),
                GirModel.Pointer => new Native.PointerParameter(parameter),
                GirModel.Class => new Native.ClassParameter(parameter),
                GirModel.Record => new Native.PointerRecordParameter(parameter),
                GirModel.PrimitiveValueType => new Native.StandardParameter(parameter),
                GirModel.Callback => new Native.CallbackParameter(parameter),
                GirModel.Enumeration => new Native.StandardParameter(parameter),
                GirModel.Bitfield => new Native.StandardParameter(parameter),

                _ => throw new Exception($"Unknown parameter type {parameter.AnyType.GetType().FullName}")
            },
            arrayType => new Native.StandardParameter(parameter)
        );

        public static IEnumerable<Parameter> CreateNativeModels(this IEnumerable<GirModel.Parameter> parameters)
            => parameters.Select(CreateNativeModel);

        private static Parameter CreateNativeModel(this GirModel.Parameter parameter) => parameter.AnyType.Match<Parameter>(
            type => type switch
            {
                GirModel.String => new Native.StringParameter(parameter),
                GirModel.Pointer => new Native.PointerParameter(parameter),
                GirModel.Class => new Native.ClassParameter(parameter),
                GirModel.Union => new Native.UnionParameter(parameter),
                GirModel.Record => new Native.SafeHandleRecordParameter(parameter),
                GirModel.PrimitiveValueType => new Native.StandardParameter(parameter),
                GirModel.Callback => new Native.CallbackParameter(parameter),
                GirModel.Enumeration => new Native.StandardParameter(parameter),
                GirModel.Bitfield => new Native.StandardParameter(parameter),
                
                _ => throw new Exception($"Unknown parameter type {parameter.AnyType.GetType().FullName}")
            },
            arrayType => new Native.StandardParameter(parameter)
        );
    }
}
