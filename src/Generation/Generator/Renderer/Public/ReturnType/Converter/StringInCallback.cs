namespace Generator.Renderer.Public.ReturnType;

internal class StringInCallback : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var type = returnType switch
        {
            { AnyTypeReference.AsT0.Type: GirModel.Utf8String, Transfer: GirModel.Transfer.None } => Model.Utf8String.GetPublicConstantStringName(),
            _ => "string"
        };
        return new RenderableReturnType(type + Nullable.Render(returnType));
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyTypeReference.References<GirModel.String>();
}
