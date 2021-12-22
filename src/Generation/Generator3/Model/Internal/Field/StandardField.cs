using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class StandardField : Field
    {
        public override string NullableTypeName => _field.IsPointer
            ? TypeNameConverter.Pointer
            : _field.AnyTypeOrCallback.AsT0.AsT0.GetName();

        public StandardField(GirModel.Field field) : base(field) { }
    }
}
