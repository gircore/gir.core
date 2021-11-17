namespace Generator3.Model.Internal
{
    public class RecordField : Field
    {
        private GirModel.Record Type => (GirModel.Record) _field.AnyTypeOrCallback.AsT0.AsT0;

        public override string NullableTypeName => _field.IsPointer 
            ? TypeMapping.Pointer
            : Type.GetFullyQualifiedInternalRecordStruct();

        public RecordField(GirModel.Field field) : base(field)
        {
            field.AnyTypeOrCallback.AsT0.VerifyType<GirModel.Record>();
        }
    }
}
