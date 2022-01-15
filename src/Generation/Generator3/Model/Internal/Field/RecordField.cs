using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class RecordField : Field
    {
        private GirModel.Record Type => (GirModel.Record) _field.AnyTypeOrCallback.AsT0.AsT0;

        public override string NullableTypeName => _field.IsPointer
            ? TypeNameConverter.Pointer
            : Type.GetFullyQualifiedInternalStructName();

        public RecordField(GirModel.Field field) : base(field)
        {
            field.AnyTypeOrCallback.AsT0.VerifyType<GirModel.Record>();
        }
    }
}
