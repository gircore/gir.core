using GirModel;

namespace Generator3.Model.Internal
{
    public class ArrayField : Field
    {
        private ArrayType ArrayType => _field.AnyTypeOrCallback.AsT0.AsT1;

        public override string? Attribute => ArrayType.FixedSize is not null
            ? MarshalAs.UnmanagedByValArray(sizeConst: ArrayType.FixedSize.Value)
            : null;

        public override string NullableTypeName => ArrayType.GetName();

        public ArrayField(GirModel.Field field) : base(field)
        {
        }
    }
}
