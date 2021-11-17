namespace Generator3.Model.Internal
{
    public class ClassReturnType : ReturnType
    {
        public override string NullableTypeName => Model.IsPointer
            ? TypeMapping.Pointer
            : Model.AnyType.AsT0.GetName();

        protected internal ClassReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Class>();
        }
    }
}
