using Generator3.Converter;

namespace Generator3.Model.Public
{
    public class InterfaceParameter : Parameter
    {
        private GirModel.Interface Type => (GirModel.Interface) Model.AnyType.AsT0;
        
        public override string NullableTypeName => Type.GetFullyQualified() + GetDefaultNullable();

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
