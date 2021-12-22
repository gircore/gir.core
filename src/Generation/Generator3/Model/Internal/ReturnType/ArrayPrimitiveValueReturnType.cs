namespace Generator3.Model.Internal
{
    public class ArrayPrimitiveValueReturnType : ReturnType
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        public override string NullableTypeName => ArrayType.GetName();

        protected internal ArrayPrimitiveValueReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyArrayType<GirModel.PrimitiveValueType>();
        }
    }
}
