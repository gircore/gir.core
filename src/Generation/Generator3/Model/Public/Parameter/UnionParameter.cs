using Generator3.Converter;

namespace Generator3.Model.Public
{
    public class UnionParameter : Parameter
    {
        private GirModel.Union Type => (GirModel.Union) Model.AnyTypeReference.AnyType.AsT0;

        public override string NullableTypeName => Type.GetFullyQualified();

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal UnionParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyTypeReference.AnyType.VerifyType<GirModel.Union>();
        }
    }
}
