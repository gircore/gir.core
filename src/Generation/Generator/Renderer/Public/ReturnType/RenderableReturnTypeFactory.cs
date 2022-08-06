namespace Generator.Renderer.Public;

internal static class RenderableReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnValue) => returnValue.AnyType.Match(
        type => type switch
        {
            GirModel.Pointer => PointerReturnType.Create(returnValue),
            GirModel.PrimitiveValueType => PrimitiveValueReturnType.Create(returnValue),
            GirModel.Class => ClassReturnType.Create(returnValue),
            GirModel.Interface => InterfaceReturnType.Create(returnValue),
            GirModel.Record => RecordReturnType.Create(returnValue),
            GirModel.Bitfield => BitfieldReturnType.Create(returnValue),
            GirModel.Enumeration => EnumerationReturnType.Create(returnValue),
            _ => StandardReturnType.Create(returnValue)
        },
        arrayType => arrayType switch
        {
            _ => StandardReturnType.Create(returnValue)
        }
    );
}
