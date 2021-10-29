using System;

namespace Generator3.Model
{
    public abstract class Parameter
    {
        public GirModel.Parameter Model { get; }

        public string Name => Model.Name;
        public abstract string NullableTypeName { get; }
        public abstract string Direction { get; }
        public virtual string Attribute => string.Empty;

        protected internal Parameter(GirModel.Parameter parameter)
        {
            Model = parameter;
        }

        protected string GetDefaultNullable() => Model.Nullable ? "?" : "";

        public static Parameter CreateForNativeCallback(GirModel.Parameter parameter) => parameter.AnyType.Match<Parameter>(
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
        
        public static Parameter CreateForNative(GirModel.Parameter parameter) => parameter.AnyType.Match<Parameter>(
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
