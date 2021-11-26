namespace Generator3.Model.Internal
{
    public class EnumerationParameter : Parameter
    {
        private GirModel.Enumeration Type => (GirModel.Enumeration) Model.AnyTypeReference.AnyType.AsT0;
        public override string NullableTypeName => Model.AnyTypeReference.IsPointer switch
        {
            true => TypeMapping.Pointer,
            false => Type.GetFullyQualified()
        };

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal EnumerationParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyTypeReference.AnyType.VerifyType<GirModel.Enumeration>();
        }
    }
}
