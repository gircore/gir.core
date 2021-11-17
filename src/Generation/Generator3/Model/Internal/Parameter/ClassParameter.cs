namespace Generator3.Model.Internal
{
    public class ClassParameter : Parameter
    {
        public override string NullableTypeName => TypeMapping.Pointer;

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal ClassParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Class>();
        }
    }
}
