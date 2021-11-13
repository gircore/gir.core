namespace Generator3.Model
{
    public static class ReturnTypeFactory
    {
        public static ReturnType CreatePublicModel(this GirModel.ReturnType returnValue) => returnValue.AnyType.Match<ReturnType>(
            type => new Public.StandardReturnType(returnValue),
            arrayType => new Public.StandardReturnType(returnValue)
        );
    }
}
