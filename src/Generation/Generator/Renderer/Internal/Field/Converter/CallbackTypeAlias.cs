namespace Generator.Renderer.Internal.Field;

internal class CallbackTypeAlias : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickT0(out var anyTypeReference, out _) && anyTypeReference.ReferencesAlias<GirModel.Callback>();
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
        var type = (GirModel.Callback) ((GirModel.Alias) field.AnyTypeReferenceOrCallback.AsT0.AsT0.Type).Type;
        return Model.Namespace.GetInternalName(type.Namespace) + "." + Model.Type.GetName(type);
    }
}
