using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class EnumerationField : Field
    {
        private GirModel.Enumeration Type => (GirModel.Enumeration) _field.AnyTypeOrCallback.AsT0.AsT0;

        public override string NullableTypeName => _field.IsPointer
            ? TypeNameConverter.Pointer
            : Type.GetFullyQualified();

        public EnumerationField(GirModel.Field field) : base(field)
        {
            field.AnyTypeOrCallback.AsT0.VerifyType<GirModel.Enumeration>();
        }
    }
}
