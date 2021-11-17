namespace Generator3.Model.Internal
{
    public class ArrayPointerRecordParameter : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        public override string NullableTypeName => ArrayType.Length is null
            ? TypeMapping.PointerArray
            : ((GirModel.Record) ArrayType.Type).GetFullyQualifiedInternalRecordStruct() + "[]";

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            : $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={Model.AnyType.AsT1.Length})]";

        protected internal ArrayPointerRecordParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
