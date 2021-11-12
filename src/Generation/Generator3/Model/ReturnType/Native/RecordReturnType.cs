namespace Generator3.Model.Native
{
    public class RecordReturnType : ReturnType
    {
        private GirModel.Record Type => (GirModel.Record) _returnValue.AnyType.AsT0;

        public override string NullableTypeName => Type.Namespace.GetNativeName() + "." + Type.GetName() + ".Handle";

        protected internal RecordReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Record>();   
        }
    }
}
