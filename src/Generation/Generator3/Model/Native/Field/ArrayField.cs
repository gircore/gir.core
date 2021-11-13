namespace Generator3.Model.Native
{
    public class ArrayField : Field
    {
        public override string? Attribute => _field.AnyTypeOrCallback.AsT0.AsT1.FixedSize is not null
            ? $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {_field.AnyTypeOrCallback.AsT0.AsT1.FixedSize})]"
            : null;

        public override string NullableTypeName => _field.AnyTypeOrCallback.AsT0.AsT1.GetName();

        public ArrayField(GirModel.Field field) : base(field)
        {
        }
    }
}
