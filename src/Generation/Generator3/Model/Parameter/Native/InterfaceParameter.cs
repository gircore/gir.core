namespace Generator3.Model.Native
{
    public class InterfaceParameter : Parameter
    {
        public override string NullableTypeName => "IntPtr";

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal InterfaceParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Interface>();
        }
    }
}
