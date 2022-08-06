namespace Generator.Renderer.Internal;

internal static class RenderableFieldFactory
{
    public static RenderableField Create(this GirModel.Field field) => field.AnyTypeOrCallback.Match(
        anyType => anyType.Match(
            type => type switch
            {
                GirModel.String => StringFieldFactory.Create(field),
                GirModel.Callback => CallbackTypeFieldFactory.Create(field),
                GirModel.Record => RecordFieldFactory.Create(field),
                GirModel.Union => UnionFieldFactory.Create(field),
                GirModel.Bitfield => BitfieldFieldFactory.Create(field),
                GirModel.Enumeration => EnumerationFieldFactory.Create(field),
                GirModel.Class => ClassFieldFactory.Create(field),
                _ => StandardFieldFactory.Create(field)
            },
            arrayType => arrayType.AnyType.Match<RenderableField>(
                type => type switch
                {
                    GirModel.Union => ArrayUnionFieldFactory.Create(field),
                    GirModel.Record => ArrayRecordFieldFactory.Create(field),
                    _ => ArrayStandardFieldFactory.Create(field)
                },
                _ => ArrayStandardFieldFactory.Create(field)
            )
        ),
        callback => CallbackFieldFactory.Create(field)
    );
}
