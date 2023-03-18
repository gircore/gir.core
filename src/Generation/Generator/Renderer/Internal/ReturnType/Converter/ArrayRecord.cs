using Generator.Model;

namespace Generator.Renderer.Internal.ReturnType;

internal class ArrayRecord : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.IsArray<GirModel.Record>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        //Internal arrays of records (SafeHandles) are not supported by the runtime and must be converted via an IntPtr[]
        return new RenderableReturnType(Type.PointerArray);
    }
}
