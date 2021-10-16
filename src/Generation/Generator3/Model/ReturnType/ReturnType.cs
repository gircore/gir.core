using System;

namespace Generator3.Model
{
    public abstract class ReturnType
    {
        protected readonly GirModel.ReturnType _returnValue;

        public abstract string NullableTypeName { get; }

        protected ReturnType(GirModel.ReturnType returnValue)
        {
            _returnValue = returnValue;
        }

        protected string GetDefaultNullable() => _returnValue.Nullable ? "?" : "";

        public static ReturnType CreateNative(GirModel.ReturnType returnValue) => returnValue.AnyType.Match<ReturnType>(
            type => type switch
            {
                GirModel.String => new Native.StringReturnType(returnValue),
                GirModel.Record => new Native.RecordReturnType(returnValue),

                _ => new Native.StandardReturnType(returnValue)
            },
            arrayType => arrayType.Type switch 
            {
                GirModel.Record => new Native.ArrayRecordReturnType(returnValue),
                _ => new Native.StandardReturnType(returnValue)
            }
        );
    }
}
