namespace Generator3.Model.Native
{
    public static class ReturnTypeFactory
    {
        public static ReturnType CreateNativeModel(this GirModel.ReturnType returnValue) => returnValue.AnyType.Match<ReturnType>(
            type => type switch
            {
                GirModel.String => new StringReturnType(returnValue),
                GirModel.Record => new RecordReturnType(returnValue),
                GirModel.Class => new ClassReturnType(returnValue),
                GirModel.Pointer => new PointerReturnType(returnValue),
                _ => new StandardReturnType(returnValue)
            },
            arrayType => arrayType.Type switch 
            {
                GirModel.Record => new ArrayRecordReturnType(returnValue),
                GirModel.Class => new ArrayClassReturnType(returnValue),
                _ => new StandardReturnType(returnValue)
            }
        );
    }
}
