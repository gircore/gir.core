namespace Generator3.Model.Native
{
    public class StandardField : Field
    {
        public override string? Attribute => null;

        public override string NullableTypeName => _field.IsPointer 
            ? TypeMapping.Pointer
            : _field.AnyTypeOrCallback.AsT0.AsT0.GetName();

        public StandardField(GirModel.Field field) : base(field) { }
    }
}
