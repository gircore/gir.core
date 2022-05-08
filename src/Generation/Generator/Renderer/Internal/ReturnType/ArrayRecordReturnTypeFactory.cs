using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class ArrayRecordReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        //Internal arrays of records (SafeHandles) are not supported by the runtime and must be converted via an IntPtr[]
        return new RenderableReturnType(Type.PointerArray);
    }
}
