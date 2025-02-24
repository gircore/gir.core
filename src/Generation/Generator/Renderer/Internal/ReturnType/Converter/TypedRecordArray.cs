using Generator.Model;

namespace Generator.Renderer.Internal.ReturnType;

internal class TypedRecordArray : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.IsArray<GirModel.Record>(out var record) && Model.Record.IsTyped(record);
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        //Internal arrays of records (SafeHandles) are not supported by the runtime and must be converted via an IntPtr[]
        return new RenderableReturnType(Type.PointerArray);
    }
}
