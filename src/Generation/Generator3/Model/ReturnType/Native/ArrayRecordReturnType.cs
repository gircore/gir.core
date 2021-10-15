namespace Generator3.Model.Native
{
    public class ArrayRecordReturnType : ReturnType
    {
        //Native arrays of records (SafeHandles) are not supported by the runtime and must be converted via an IntPtr[]
        public override string NullableTypeName => "IntPtr[]";

        protected internal ArrayRecordReturnType(GirModel.ReturnType returnValue) : base(returnValue) { }
    }
}
