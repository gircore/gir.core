namespace Generator3.Model.Internal
{
    public class EnumerationParameter : Parameter
    {
        private GirModel.Enumeration Type => (GirModel.Enumeration) Model.AnyTypeReference.AnyType.AsT0;
        public override string NullableTypeName => Model.AnyTypeReference.IsPointer switch
        {
            true => TypeMapping.Pointer,
            //Internal does not define any enumerations. They are part of the Public API to avoid converting between them.
            false => Type.Namespace.Name + "." + Type.GetName()
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
