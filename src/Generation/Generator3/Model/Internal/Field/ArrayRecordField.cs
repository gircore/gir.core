using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ArrayRecordField : Field
    {
        private GirModel.ArrayType ArrayType => _field.AnyTypeOrCallback.AsT0.AsT1;
        private GirModel.Record Type => (GirModel.Record) ArrayType.AnyType.AsT0;

        public override string? Attribute => ArrayType.FixedSize is not null
            ? MarshalAs.UnmanagedByValArray(sizeConst: ArrayType.FixedSize.Value)
            : null;

        public override string NullableTypeName => Type.GetFullyQualifiedInternalStructName() + "[]";

        public ArrayRecordField(GirModel.Field field) : base(field)
        {
            field.AnyTypeOrCallback.AsT0.VerifyArrayType<GirModel.Record>();
        }
    }
}
