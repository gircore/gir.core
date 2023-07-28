using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class Record : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var nullableTypeName = ComplexType.GetFullyQualified((GirModel.Record) returnType.AnyType.AsT0);

        if (returnType.Nullable)
            nullableTypeName = $"{nullableTypeName}?";
        return new RenderableReturnType(nullableTypeName);
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyType.Is<GirModel.Record>();
}
