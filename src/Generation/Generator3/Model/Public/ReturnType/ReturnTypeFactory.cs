namespace Generator3.Model.Public
{
    public static class ReturnTypeFactory
    {
        public static ReturnType CreatePublicModel(this GirModel.ReturnType returnValue) => returnValue.AnyType.Match<ReturnType>(
            type => type switch
            {
                GirModel.Record => new RecordReturnType(returnValue),
                _ => new StandardReturnType(returnValue)
            },
            arrayType => arrayType.AnyTypeReference switch 
            {
                _ => new StandardReturnType(returnValue)
            }
        );
    }
}
