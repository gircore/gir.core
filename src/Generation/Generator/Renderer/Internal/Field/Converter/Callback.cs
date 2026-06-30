namespace Generator.Renderer.Internal.Field;

internal class Callback : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.IsT1;
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: Model.Callback.GetInternalDelegateName(field.AnyTypeOrCallback.AsT1),
            Array: null
        )];
    }
}
