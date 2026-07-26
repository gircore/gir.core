using Generator.Model;

namespace Generator.Renderer.Public;

/// <summary>
/// Renders the conversion of a single element of a GLib.List or GLib.SList
/// into its managed representation.
/// </summary>
/// <remarks>
/// The elements of a container are only owned by the runtime if the whole
/// container is transferred with <see cref="GirModel.Transfer.Full"/>. An owned element
/// is adopted by its managed representation which frees it. An unowned element
/// must be copied because it is not guaranteed to outlive the container.
/// </remarks>
internal static class ListElement
{
    public static string GetPublicTypeName(GirModel.AnyType elementType) => elementType switch
    {
        _ when elementType.Is<GirModel.Class>(out var cls) => ComplexType.GetFullyQualified(cls),
        _ when elementType.Is<GirModel.Interface>(out var @interface) => Model.Type.GetPublicNameFullyQuallified(@interface),
        _ when elementType.Is<GirModel.Utf8String>() => "string",
        _ when elementType.Is<GirModel.Record>(out var record) => ComplexType.GetFullyQualified(record),
        _ => throw new System.NotImplementedException($"Can't render {elementType} as list element")
    };

    public static string RenderCreate(GirModel.AnyType elementType, GirModel.Transfer transfer, string dataVariableName)
    {
        //Only a container which is transferred completely owns its elements.
        var isOwned = transfer == GirModel.Transfer.Full;

        return elementType switch
        {
            _ when elementType.Is<GirModel.Class>(out var cls) => RenderWrapHandle(
                publicTypeName: ComplexType.GetFullyQualified(cls),
                fallbackTypeName: ComplexType.GetFullyQualified(cls),
                dataVariableName, isOwned),

            _ when elementType.Is<GirModel.Interface>(out var @interface) => RenderWrapHandle(
                publicTypeName: Model.Type.GetPublicNameFullyQuallified(@interface),
                fallbackTypeName: Model.Interface.GetFullyQualifiedImplementationName(@interface),
                dataVariableName, isOwned),

            _ when elementType.Is<GirModel.Utf8String>() => isOwned
                ? $"GLib.Internal.StringHelper.ToStringUtf8AndFree({dataVariableName})"
                : $"GLib.Internal.StringHelper.ToStringUtf8({dataVariableName})",

            _ when elementType.Is<GirModel.Record>(out var record) => RenderCreateRecord(record, dataVariableName, isOwned),

            _ => throw new System.NotImplementedException($"Can't render {elementType} as list element")
        };
    }

    private static string RenderWrapHandle(string publicTypeName, string fallbackTypeName, string dataVariableName, bool isOwned)
        => $"({publicTypeName}) GObject.Internal.InstanceWrapper.WrapHandle<{fallbackTypeName}>({dataVariableName}, {isOwned.ToString().ToLower()})";

    private static string RenderCreateRecord(GirModel.Record record, string dataVariableName, bool isOwned)
    {
        var ownedHandle = Model.Record.IsOpaqueTyped(record)
            ? Model.OpaqueTypedRecord.GetFullyQuallifiedOwnedHandle(record)
            : Model.TypedRecord.GetFullyQuallifiedOwnedHandle(record);

        var unownedHandle = Model.Record.IsOpaqueTyped(record)
            ? Model.OpaqueTypedRecord.GetFullyQuallifiedUnownedHandle(record)
            : Model.TypedRecord.GetFullyQuallifiedUnownedHandle(record);

        var handleExpression = isOwned
            ? $"new {ownedHandle}({dataVariableName})"
            : $"new {unownedHandle}({dataVariableName}).OwnedCopy()";

        return $"new {ComplexType.GetFullyQualified(record)}({handleExpression})";
    }
}
