namespace Generator3.Model.Native
{
    public class RecordReturnType : ReturnType
    {
        public override string NullableTypeName => _returnValue.Type.GetName() + ".Handle";

        protected internal RecordReturnType(GirModel.ReturnType returnValue) : base(returnValue) { }
    }
}
