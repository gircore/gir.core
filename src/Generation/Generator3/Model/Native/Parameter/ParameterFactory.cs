using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model.Native
{
    public static class ParameterFactory
    {
        public static IEnumerable<Parameter> CreateNativeModelsForCallback(this IEnumerable<GirModel.Parameter> parameters)
            => parameters.Select(CreateNativeModelForCallback);

        private static Parameter CreateNativeModelForCallback(this GirModel.Parameter parameter) => parameter.AnyType.Match<Parameter>(
            type => type switch
            {
                //Callbacks do not support record safe handles in parameters
                GirModel.Record => new Native.PointerRecordParameter(parameter),
                _ => CreateNativeModel(parameter)
            },
            arrayType => CreateNativeModel(parameter)
        );

        public static IEnumerable<Parameter> CreateNativeModelsForMethod(this IEnumerable<GirModel.Parameter> parameters)
            => parameters.Select(CreateNativeModelForMethod);
        
        private static Parameter CreateNativeModelForMethod(this GirModel.Parameter parameter) => parameter.AnyType.Match<Parameter>(
            type => CreateNativeModel(parameter),
            arrayType => arrayType.Type switch
            {
                //Methods need special handling because of their instance parameters
                GirModel.Record => new Native.ArrayPointerRecordParameterForMethod(parameter),
                _ => CreateNativeModel(parameter)  
            }
        );
        
        public static IEnumerable<Parameter> CreateNativeModels(this IEnumerable<GirModel.Parameter> parameters)
            => parameters.Select(CreateNativeModel);

        private static Parameter CreateNativeModel(this GirModel.Parameter parameter) => parameter.AnyType.Match<Parameter>(
            type => type switch
            {
                GirModel.String => new Native.StringParameter(parameter),
                GirModel.Pointer => new Native.PointerParameter(parameter),
                GirModel.UnsignedPointer => new Native.UnsignedPointerParameter(parameter),
                GirModel.Class => new Native.ClassParameter(parameter),
                GirModel.Interface => new Native.InterfaceParameter(parameter),
                GirModel.Union => new Native.UnionParameter(parameter),
                GirModel.Record => new Native.SafeHandleRecordParameter(parameter),
                GirModel.PrimitiveValueType => new Native.StandardParameter(parameter),
                GirModel.Callback => new Native.CallbackParameter(parameter),
                GirModel.Enumeration => new Native.StandardParameter(parameter),
                GirModel.Bitfield => new Native.StandardParameter(parameter),
                GirModel.Void => new Native.VoidParameter(parameter),
                
                _ => throw new Exception($"Parameter \"{parameter.Name}\" of type {parameter.AnyType} can not be converted into a model")
            },
            arrayType => arrayType.Type switch
            {
                GirModel.Record => new Native.ArrayPointerRecordParameter(parameter),
                _ => new Native.StandardParameter(parameter)   
            }
        );
    }
}
