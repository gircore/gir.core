namespace Generator3.Model.Native
{
    public class PointerReturnType : ReturnType
    {
        public override string NullableTypeName => TypeMapping.Pointer;

        protected internal PointerReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Pointer>();   
        }
    }
}
