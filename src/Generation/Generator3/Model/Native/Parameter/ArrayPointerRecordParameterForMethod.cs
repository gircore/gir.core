namespace Generator3.Model.Native
{
    public class ArrayPointerRecordParameterForMethod : Parameter
    {
        public override string NullableTypeName => TypeMapping.PointerArray;

        public override string Attribute => Model.AnyType.AsT1.Length is null
            ? string.Empty
            //We add 1 to the length because Methods contain an instance parameter which is not counted
            : $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={Model.AnyType.AsT1.Length + 1})]";

        protected internal ArrayPointerRecordParameterForMethod(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
