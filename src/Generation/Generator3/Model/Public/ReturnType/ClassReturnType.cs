namespace Generator3.Model.Public
{
    public class ClassReturnType : ReturnType
    {
        private GirModel.Class Type => (GirModel.Class) Model.AnyType.AsT0;

        public override string NullableTypeName => Type.GetFullyQualified();

        protected internal ClassReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Class>();
        }
    }
}
