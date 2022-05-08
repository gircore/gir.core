using Generator.Model;

namespace Generator.Renderer.Public;

internal static class RecordReturnType
{
    public static RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = ComplexType.GetFullyQualified((GirModel.Record) returnType.AnyType.AsT0);

        return new RenderableReturnType(nullableTypeName);
    }
}
