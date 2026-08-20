using Generator.Model;

namespace Generator.Renderer.Internal.Field;

internal class Class : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeReferenceOrCallback.TryPickT0(out var anyTypeReference, out _) && anyTypeReference.References<GirModel.Class>();
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
        var type = (GirModel.Class) field.AnyTypeReferenceOrCallback.AsT0.AsT0.Type;
        return field.IsPointer
            ? Type.Pointer
            : Model.Class.GetFullyQualifiedInternalStructName(type);
    }
}
