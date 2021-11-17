namespace Generator3.Model.Internal
{
    public class ArrayRecordReturnType : ReturnType
    {
        //Internal arrays of records (SafeHandles) are not supported by the runtime and must be converted via an IntPtr[]
        public override string NullableTypeName => TypeMapping.PointerArray;

        protected internal ArrayRecordReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
