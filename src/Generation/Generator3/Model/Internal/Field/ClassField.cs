using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ClassField : Field
    {
        private GirModel.Class Type => (GirModel.Class) _field.AnyTypeOrCallback.AsT0.AsT0;

        public override string NullableTypeName => _field.IsPointer 
            ? TypeNameConverter.Pointer
            : Type.GetFullyQualifiedInternalStruct();

        public ClassField(GirModel.Field field) : base(field)
        {
            field.AnyTypeOrCallback.AsT0.VerifyType<GirModel.Class>();
        }
    }
}
