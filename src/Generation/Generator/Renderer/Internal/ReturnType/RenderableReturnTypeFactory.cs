namespace Generator.Renderer.Internal;

internal static class RenderableReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnValue) => returnValue.AnyType.Match(
        type => type switch
        {
            GirModel.PrimitiveValueType => PrimitiveValueReturnTypeFactory.Create(returnValue),
            GirModel.Bitfield => BitfieldReturnTypeFactory.Create(returnValue),
            GirModel.Enumeration => EnumerationReturnTypeFactory.Create(returnValue),
            GirModel.Utf8String => Utf8StringReturnTypeFactory.Create(returnValue),
            GirModel.PlatformString => PlatformStringReturnTypeFactory.Create(returnValue),
            GirModel.Record => RecordReturnTypeFactory.Create(returnValue),
            GirModel.Union => UnionReturnTypeFactory.Create(returnValue),
            GirModel.Class => ClassReturnTypeFactory.Create(returnValue),
            GirModel.Interface => InterfaceReturnTypeFactory.Create(returnValue),
            GirModel.Pointer => PointerReturnTypeFactory.Create(returnValue),
            _ => StandardReturnTypeFactory.Create(returnValue)
        },
        arrayType => arrayType.AnyType.Match<RenderableReturnType>(
            type => type switch
            {
                GirModel.String => ArrayStringReturnTypeFactory.Create(returnValue),
                GirModel.Record => ArrayRecordReturnTypeFactory.Create(returnValue),
                GirModel.Class => ArrayClassReturnTypeFactory.Create(returnValue),
                GirModel.PrimitiveValueType => ArrayPrimitiveValueReturnTypeFactory.Create(returnValue),
                _ => StandardReturnTypeFactory.Create(returnValue)
            },
            _ => StandardReturnTypeFactory.Create(returnValue)
        )
    );

    public static RenderableReturnType CreateForCallback(this GirModel.ReturnType returnValue) => returnValue.AnyType.Match(
        type => type switch
        {
            GirModel.PrimitiveValueType => PrimitiveValueReturnTypeFactory.Create(returnValue),
            GirModel.Bitfield => BitfieldReturnTypeFactory.Create(returnValue),
            GirModel.Enumeration => EnumerationReturnTypeFactory.Create(returnValue),
            GirModel.Utf8String => Utf8StringReturnTypeFactory.CreateForCallback(returnValue),
            GirModel.PlatformString => PlatformStringReturnTypeFactory.CreateForCallback(returnValue),
            GirModel.Record => RecordReturnTypeForCallbackFactory.Create(returnValue),
            GirModel.Union => UnionReturnTypeFactory.Create(returnValue),
            GirModel.Class => ClassReturnTypeFactory.Create(returnValue),
            GirModel.Interface => InterfaceReturnTypeFactory.Create(returnValue),
            GirModel.Pointer => PointerReturnTypeFactory.Create(returnValue),
            _ => StandardReturnTypeFactory.Create(returnValue)
        },
        arrayType => arrayType.AnyType.Match<RenderableReturnType>(
            type => type switch
            {
                GirModel.String => ArrayStringReturnTypeFactory.Create(returnValue),
                GirModel.Record => ArrayRecordReturnTypeFactory.Create(returnValue),
                GirModel.Class => ArrayClassReturnTypeFactory.Create(returnValue),
                GirModel.PrimitiveValueType => ArrayPrimitiveValueReturnTypeFactory.Create(returnValue),
                _ => StandardReturnTypeFactory.Create(returnValue)
            },
            _ => StandardReturnTypeFactory.Create(returnValue)
        )
    );
}
