namespace Generator3.Model.Internal
{
    public class ArrayPointerRecordParameterForMethod : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        public override string NullableTypeName => ArrayType.Length is null
            ? TypeMapping.PointerArray
            : ((GirModel.Record) ArrayType.Type).GetFullyQualifiedInternalRecordStruct() + "[]";

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            //We add 1 to the length because Methods contain an instance parameter which is not counted
            : $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={Model.AnyType.AsT1.Length + 1})]";

        protected internal ArrayPointerRecordParameterForMethod(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
