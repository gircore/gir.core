namespace Generator3.Model.Internal
{
    public class ArrayRecordParameter : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyTypeReference.AnyType.AsT1;

        public override string NullableTypeName => ArrayType.Length is null
            ? TypeMapping.PointerArray
            : ((GirModel.Record) ArrayType.AnyTypeReference.AnyType.AsT0).GetFullyQualifiedInternalRecordStruct() + "[]";

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            : MarshalAs.UnmanagedLpArray(sizeParamIndex: ArrayType.Length.Value);

        protected internal ArrayRecordParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyTypeReference.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
