using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ArrayUnionField : Field
    {
        private GirModel.ArrayType ArrayType => _field.AnyTypeOrCallback.AsT0.AsT1;
        private GirModel.Union Type => (GirModel.Union) ArrayType.AnyType.AsT0;

        public override string? Attribute => ArrayType.FixedSize is not null
            ? MarshalAs.UnmanagedByValArray(sizeConst: ArrayType.FixedSize.Value)
            : null;

        public override string NullableTypeName => Type.GetFullyQualifiedInternalStructName() + "[]";

        public ArrayUnionField(GirModel.Field field) : base(field)
        {
            field.AnyTypeOrCallback.AsT0.VerifyArrayType<GirModel.Union>();
        }
    }
}
