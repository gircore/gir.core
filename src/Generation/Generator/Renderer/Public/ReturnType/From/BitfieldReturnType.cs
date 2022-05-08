using Generator.Model;

namespace Generator.Renderer.Public;

internal static class BitfieldReturnType
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = ComplexType.GetFullyQualified((GirModel.Bitfield) returnType.AnyType.AsT0);

        return new RenderableReturnType(nullableTypeName);
    }
}
