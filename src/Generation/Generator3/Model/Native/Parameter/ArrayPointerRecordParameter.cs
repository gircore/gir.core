namespace Generator3.Model.Native
{
    public class ArrayPointerRecordParameter : Parameter
    {
        public override string NullableTypeName => TypeMapping.PointerArray;

        public override string Attribute => Model.AnyType.AsT1.Length is null
            ? string.Empty
            : $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={Model.AnyType.AsT1.Length})]";

        protected internal ArrayPointerRecordParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
