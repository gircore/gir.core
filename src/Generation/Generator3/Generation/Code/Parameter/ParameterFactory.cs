using System;

namespace Generator3.Generation.Code
{
    public static class ParameterFactory
    {
        public static Parameter CreateNative(GirModel.Parameter parameter) => parameter switch
        {
            { Type: GirModel.String } => new Native.StandardParameter(parameter),
            { Type: GirModel.Pointer } => new Native.PointerParameter(parameter),
            { Type: GirModel.Class } => new Native.ClassParameter(parameter),
            { Type: GirModel.Record } => new Native.RecordParameter(parameter),
            { Type: GirModel.PrimitiveValueType } => new Native.StandardParameter(parameter),
            { Type: GirModel.Callback } => new Native.CallbackParameter(parameter),
            { Type: GirModel.Enumeration } => new Native.StandardParameter(parameter),
            { Type: GirModel.Bitfield } => new Native.StandardParameter(parameter),
            _ => throw new Exception($"Unknown parameter type {parameter.Type.GetType().FullName}")
        };
    }
}
