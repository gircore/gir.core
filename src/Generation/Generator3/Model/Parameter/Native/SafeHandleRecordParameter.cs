namespace Generator3.Model.Native
{
    public class SafeHandleRecordParameter : Parameter
    {
        //Native records are represented as SafeHandles and are not nullable
        public override string NullableTypeName => Model.AnyType.AsT0.GetName() + ".Handle";

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.In,
            @inout: ParameterDirection.In
        );

        protected internal SafeHandleRecordParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Record>();
        }
    }
}
