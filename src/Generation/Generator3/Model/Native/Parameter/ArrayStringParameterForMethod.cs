namespace Generator3.Model.Native
{
    public class ArrayStringParameterForMethod : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        public override string NullableTypeName => ArrayType.GetName();

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            //We add 1 to the length because Methods contain an instance parameter which is not counted
            : $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={ArrayType.Length+1})]";

        protected internal ArrayStringParameterForMethod(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.String>();
        }
    }
}
