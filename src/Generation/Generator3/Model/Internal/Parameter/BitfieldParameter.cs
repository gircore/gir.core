using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class BitfieldParameter : Parameter
    {
        private GirModel.Bitfield Type => (GirModel.Bitfield) Model.AnyType.AsT0;
        public override string NullableTypeName => Model.IsPointer switch
        {
            true => TypeNameConverter.Pointer,
            //Internal does not define any bitfields. They are part of the Public API to avoid converting between them.
            false => Type.GetFullyQualified()
        };

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
