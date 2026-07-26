namespace Generator.Renderer.Internal.ReturnType;

internal class GLibList : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return Model.ListType.SupportsReturnValue(returnType);
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var record = (GirModel.Record) returnType.AnyType.AsT0;

        return new RenderableReturnType(Model.ListType.GetInternalHandleName(record, returnType.Transfer));
    }
}
