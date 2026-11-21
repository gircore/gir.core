namespace Generator.Renderer.Internal.Field;

internal class CallbackType : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickAnyTypeReference(out var anyTypeReference) && anyTypeReference.References<GirModel.Callback>();
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        return [new RenderableField(
            Name: Model.Field.GetName(field),
            TypeName: GetTypeName(field),
            Array: null
        )];
    }

    private static string GetTypeName(GirModel.Field field)
    {
        var type = (GirModel.Callback) field.AnyTypeReferenceOrCallback.AsAnyTypeReference.AsT0.Type;
        return Model.Namespace.GetInternalName(type.Namespace) + "." + Model.Type.GetName(type);
    }
}
