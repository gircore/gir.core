using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class BitfieldField : Field
    {
        private GirModel.Bitfield Type => (GirModel.Bitfield) _field.AnyTypeOrCallback.AsT0.AsT0;

        public override string NullableTypeName => _field.IsPointer 
            ? TypeNameConverter.Pointer
            : Type.GetFullyQualified();
        
        public BitfieldField(GirModel.Field field) : base(field)
        {
            field.AnyTypeOrCallback.AsT0.VerifyType<GirModel.Bitfield>();
        }
    }
}
