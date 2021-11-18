namespace Generator3.Model.Internal
{
    public class ArrayRecordParameterForMethod : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyTypeReference.AnyType.AsT1;

        public override string NullableTypeName => ArrayType.Length is null
            ? TypeMapping.PointerArray
            : ((GirModel.Record) ArrayType.AnyTypeReference.AnyType.AsT0).GetFullyQualifiedInternalRecordStruct() + "[]";

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            //We add 1 to the length because Methods contain an instance parameter which is not counted
            : MarshalAs.UnmanagedLpArray(sizeParamIndex: ArrayType.Length.Value + 1);

        protected internal ArrayRecordParameterForMethod(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyTypeReference.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
