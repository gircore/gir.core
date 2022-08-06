using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class ArrayRecordFieldFactory
{
    public static RenderableField Create(GirModel.Field field)
    {
        return new RenderableField(
            Name: Field.GetName(field),
            Attribute: GetAttribute(field),
            NullableTypeName: GetNullableTypeName(field)
        );
    }

    private static string? GetAttribute(GirModel.Field field)
    {
        var arrayType = field.AnyTypeOrCallback.AsT0.AsT1;
        return arrayType.FixedSize is not null
            ? MarshalAs.UnmanagedByValArray(sizeConst: arrayType.FixedSize.Value)
            : null;
    }

    private static string GetNullableTypeName(GirModel.Field field)
    {
        var arrayType = field.AnyTypeOrCallback.AsT0.AsT1;
        var type = (GirModel.Record) arrayType.AnyType.AsT0;
        return Record.GetFullyQualifiedInternalStructName(type) + "[]";
    }
}
