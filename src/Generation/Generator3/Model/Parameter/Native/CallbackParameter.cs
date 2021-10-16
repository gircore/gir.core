using System;

namespace Generator3.Model.Native
{
    public class CallbackParameter : Parameter
    {
        //Native callbacks are not nullable and have a suffix "Callback"
        public override string NullableTypeName => Model.AnyType.Match(
            type => type.GetName() + "Callback",
            _ => throw new Exception($"{nameof(CallbackParameter)} does not support array types")
        );

        public override string Direction => ParameterDirection.In;

        protected internal CallbackParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Callback>();
        }
    }
}
