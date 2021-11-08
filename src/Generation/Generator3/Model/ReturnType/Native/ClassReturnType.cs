namespace Generator3.Model.Native
{
    public class ClassReturnType : ReturnType
    {
        public override string NullableTypeName => _returnValue.IsPointer
            ? TypeMapping.Pointer
            : _returnValue.AnyType.AsT0.GetName();

        protected internal ClassReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Class>();
        }
    }
}
