namespace Generator3.Model.Native
{
    public class UnsignedPointerParameter : Parameter
    {
        //IntPtr can't be nullable. They can be "nulled" via IntPtr.Zero.
        public override string NullableTypeName => Model.AnyType.AsT0.GetName();

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal UnsignedPointerParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.UnsignedPointer>();
        }
    }
}
