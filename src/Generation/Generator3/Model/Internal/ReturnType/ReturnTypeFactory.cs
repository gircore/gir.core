namespace Generator3.Model.Internal
{
    public static class ReturnTypeFactory
    {
        public static ReturnType CreateInternalModel(this GirModel.ReturnType returnValue) => returnValue.AnyType.Match<ReturnType>(
            type => type switch
            {
                GirModel.PrimitiveValueType => new PrimitiveValueReturnType(returnValue),
                GirModel.Bitfield => new BitfieldReturnType(returnValue),
                GirModel.Enumeration => new EnumerationReturnType(returnValue),
                GirModel.Utf8String => new Utf8StringReturnType(returnValue),
                GirModel.PlatformString => new PlatformStringReturnType(returnValue),
                GirModel.Record => new RecordReturnType(returnValue),
                GirModel.Union => new UnionReturnType(returnValue),
                GirModel.Class => new ClassReturnType(returnValue),
                GirModel.Interface => new InterfaceReturnType(returnValue),
                GirModel.Pointer => new PointerReturnType(returnValue),
                _ => new StandardReturnType(returnValue)
            },
            arrayType => arrayType.AnyTypeReference.AnyType.Match<ReturnType>(
                type => type switch 
                {
                    GirModel.String => new ArrayStringReturnType(returnValue),
                    GirModel.Record => new ArrayRecordReturnType(returnValue),
                    GirModel.Class => new ArrayClassReturnType(returnValue),
                    GirModel.PrimitiveValueType => new ArrayPrimitiveValueReturnType(returnValue),
                    _ => new StandardReturnType(returnValue)
                },
                _ => new StandardReturnType(returnValue)
            ) 
        );
    }
}
