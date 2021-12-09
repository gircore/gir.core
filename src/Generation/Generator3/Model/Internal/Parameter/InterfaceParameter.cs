using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class InterfaceParameter : Parameter
    {
        public override string NullableTypeName => TypeNameConverter.Pointer;

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal InterfaceParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyTypeReference.AnyType.VerifyType<GirModel.Interface>();
        }
    }
}
