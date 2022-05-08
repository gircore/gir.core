using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class ArrayStringReturnTypeFactory
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var arrayType = returnType.AnyType.AsT1;
        var isMarshalAble = returnType.Transfer != GirModel.Transfer.None || arrayType.Length != null;
        var nullableTypeName = isMarshalAble
            ? ArrayType.GetName(arrayType)
            : Type.Pointer;

        return new RenderableReturnType(nullableTypeName);
    }
}
