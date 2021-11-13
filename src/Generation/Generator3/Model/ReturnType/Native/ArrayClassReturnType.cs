namespace Generator3.Model.Native
{
    public class ArrayClassReturnType : ReturnType
    {
        public override string NullableTypeName => TypeMapping.PointerArray;

        protected internal ArrayClassReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyArrayType<GirModel.Class>();
        }
    }
}
