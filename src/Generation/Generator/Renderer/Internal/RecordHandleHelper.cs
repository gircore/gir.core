using System.Text;
using Generator.Renderer.Internal.Field;

namespace Generator.Renderer.Internal;

internal class RecordHandleHelper
{
    public static string RenderField(GirModel.Record record, GirModel.Field field)
    {
        if (field is { IsReadable: false, IsWritable: false } || field.IsPrivate)
            return string.Empty;

        var renderableFields = Fields.GetRenderableField(field);
        var result = new StringBuilder();

        foreach (var renderableField in renderableFields)
        {
            if (field.IsReadable)
                result.AppendLine(RenderFieldGetter(record, field, renderableField));

            if (field.IsWritable)
                result.AppendLine(RenderFieldSetter(record, field, renderableField));
        }

        return result.ToString();
    }

    private static string RenderFieldGetter(GirModel.Record record, GirModel.Field field, RenderableField renderableField)
    {
        if (renderableField.Array?.FixedSize.HasValue ?? false)
            return RenderFieldGetterFixedSizeArray(record, field, renderableField);

        return RenderFieldGetterDefault(record, field, renderableField);
    }

    private static string RenderFieldSetter(GirModel.Record record, GirModel.Field field, RenderableField renderableField)
    {
        if (renderableField.Array?.FixedSize.HasValue ?? false)
            return RenderFieldSetterFixedSizeArray(record, field, renderableField);

        return RenderFieldSetterDefault(record, renderableField);
    }

    private static string RenderFieldGetterFixedSizeArray(GirModel.Record record, GirModel.Field field, RenderableField renderableField)
    {
        var type = (field.AnyTypeOrCallback.IsT1, renderableField.Array?.FixedSize.HasValue) switch
        {
            (true, _) => $"{Model.TypedRecord.GetDataName(record)}.{renderableField.GetTypeName()}",
            (_, true) => $"{Model.TypedRecord.GetDataName(record)}.{renderableField.GetInlineArrayTypeName()}",
            _ => renderableField.GetTypeName()
        };

        var dataName = Model.TypedRecord.GetDataName(record);

        return @$"public {type} Get{renderableField.Name}()
{{
    if (IsClosed || IsInvalid)
        throw new InvalidOperationException(""Handle is closed or invalid"");

    return Marshal.PtrToStructure<{dataName}>(handle).{renderableField.Name};
}}";
    }

    private static string RenderFieldGetterDefault(GirModel.Record record, GirModel.Field field, RenderableField renderableField)
    {
        var typePrefix = field.AnyTypeOrCallback.IsT1 ? $"{Model.TypedRecord.GetDataName(record)}." : string.Empty;
        var dataName = Model.TypedRecord.GetDataName(record);

        return @$"public {typePrefix}{renderableField.GetTypeName()} Get{renderableField.Name}()
{{
    if (IsClosed || IsInvalid)
        throw new InvalidOperationException(""Handle is closed or invalid"");

    return Marshal.PtrToStructure<{dataName}>(handle).{renderableField.Name};
}}";
    }

    private static string RenderFieldSetterFixedSizeArray(GirModel.Record record, GirModel.Field field, RenderableField renderableField)
    {
        var type = renderableField.Array?.FixedSize.HasValue ?? false
            ? $"{Model.TypedRecord.GetDataName(record)}.{renderableField.GetInlineArrayTypeName()}"
            : renderableField.GetTypeName();

        var dataName = Model.TypedRecord.GetDataName(record);

        return @$"public void Set{renderableField.Name}({type} value)
{{
    if (IsClosed || IsInvalid)
        throw new InvalidOperationException(""Handle is closed or invalid"");

    var data = Marshal.PtrToStructure<{dataName}>(handle);
    data.{renderableField.Name} = value;
    Marshal.StructureToPtr(data, handle, false);
}}";
    }

    private static string RenderFieldSetterDefault(GirModel.Record record, RenderableField renderableField)
    {
        var dataName = Model.TypedRecord.GetDataName(record);

        return @$"public void Set{renderableField.Name}({renderableField.GetTypeName()} value)
{{
    if (IsClosed || IsInvalid)
        throw new InvalidOperationException(""Handle is closed or invalid"");

    var data = Marshal.PtrToStructure<{dataName}>(handle);
    data.{renderableField.Name} = value;
    Marshal.StructureToPtr(data, handle, false);
}}";
    }
}
