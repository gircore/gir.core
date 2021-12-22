using Generator3.Converter;

namespace Generator3.Model.Public
{
    public class BitfieldParameter : Parameter
    {
        private GirModel.Bitfield Type => (GirModel.Bitfield) Model.AnyType.AsT0;

        public override string NullableTypeName => Type.GetFullyQualified();

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal BitfieldParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Bitfield>();
        }
    }
}
