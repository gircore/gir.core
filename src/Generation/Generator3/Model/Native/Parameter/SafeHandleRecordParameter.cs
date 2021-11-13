namespace Generator3.Model.Native
{
    public class SafeHandleRecordParameter : Parameter
    {
        private GirModel.Record Type => (GirModel.Record) Model.AnyType.AsT0;
        
        //Native records are represented as SafeHandles and are not nullable
        public override string NullableTypeName => Type.Namespace.GetNativeName() + "." + Type.GetName() + ".Handle";

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
