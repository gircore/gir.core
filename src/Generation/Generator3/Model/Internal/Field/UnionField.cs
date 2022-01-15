using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class UnionField : Field
    {
        private GirModel.Union Type => (GirModel.Union) _field.AnyTypeOrCallback.AsT0.AsT0;

        public override string NullableTypeName => _field.IsPointer
            ? TypeNameConverter.Pointer
            : Type.GetFullyQualifiedInternalStructName();

        public UnionField(GirModel.Field field) : base(field)
        {
            field.AnyTypeOrCallback.AsT0.VerifyType<GirModel.Union>();
        }
    }
}
