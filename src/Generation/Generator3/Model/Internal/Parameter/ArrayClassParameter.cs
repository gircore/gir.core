namespace Generator3.Model.Internal
{
    public class ArrayClassParameter : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyTypeReference.AnyType.AsT1;

        //Classes are always passed as a pointer. 
        public override string NullableTypeName => ArrayType.Length is null
            ? TypeMapping.Pointer
            : TypeMapping.PointerArray;

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            : MarshalAs.UnmanagedLpArray(sizeParamIndex: ArrayType.Length.Value);

        protected internal ArrayClassParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyTypeReference.AnyType.VerifyArrayType<GirModel.Class>();
        }
    }
}
