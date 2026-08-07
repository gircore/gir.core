using System.Diagnostics.CodeAnalysis;

namespace Generator.Model;

/// <summary>
/// GLib.List and GLib.SList are container types. In contrast to arrays they don't
/// store their element type in the type system but only as an annotation in the
/// gir file. Without a known element type the elements can't be converted into
/// managed representations.
/// </summary>
internal static class ListType
{
    public static bool IsSingleLinked(GirModel.Record record)
        => record.Name == "SList";

    /// <summary>
    /// The handle of a list returned by native code. A list which is not owned by
    /// the runtime is never freed. In all other cases the elements are converted into
    /// managed representations which take care of the element memory, so only the
    /// container itself is left to be freed.
    /// </summary>
    public static string GetInternalHandleName(GirModel.Record record, GirModel.Transfer transfer)
    {
        var name = IsSingleLinked(record) ? "SList" : "List";

        return transfer == GirModel.Transfer.None
            ? $"GLib.Internal.{name}UnownedHandle"
            : $"GLib.Internal.{name}ContainerHandle";
    }

    /// <summary>
    /// Returns the element type of the given container. A list has exactly one element
    /// type. It is missing if the gir file does not annotate it.
    /// </summary>
    public static bool TryGetElementType(GirModel.ReturnType returnType, [NotNullWhen(true)] out GirModel.AnyType? elementType)
    {
        elementType = returnType.AnyType.IsGLibList() && returnType.ElementTypes.Count == 1
            ? returnType.ElementTypes[0]
            : null;

        return elementType is not null;
    }

    /// <summary>
    /// Checks whether the elements of a container of the given type can be converted
    /// into managed representations which own their memory.
    /// </summary>
    public static bool SupportsElementType(GirModel.AnyType elementType) => elementType switch
    {
        //Fundamental classes are not reference counted like standard classes.
        _ when elementType.Is<GirModel.Class>(out var cls) => !cls.Fundamental,
        _ when elementType.Is<GirModel.Interface>() => true,
        _ when elementType.Is<GirModel.Utf8String>() => true,
        _ when elementType.Is<GirModel.Record>(out var record) => Record.IsTyped(record) || Record.IsOpaqueTyped(record),
        _ => false
    };

    public static bool SupportsReturnValue(GirModel.ReturnType returnType)
        => TryGetElementType(returnType, out var elementType) && SupportsElementType(elementType);
}
