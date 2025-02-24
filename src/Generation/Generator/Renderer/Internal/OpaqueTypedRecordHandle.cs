using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class OpaqueTypedRecordHandle
{
    public static string Render(GirModel.Record record)
    {
        var typeName = Model.OpaqueTypedRecord.GetInternalHandle(record);
        var unownedHandleTypeName = Model.OpaqueTypedRecord.GetInternalUnownedHandle(record);
        var ownedHandleTypeName = Model.OpaqueTypedRecord.GetInternalOwnedHandle(record);

        return $@"using System;
using GObject;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

#nullable enable

namespace {Namespace.GetInternalName(record.Namespace)};

// AUTOGENERATED FILE - DO NOT MODIFY

{PlatformSupportAttribute.Render(record as GirModel.PlatformDependent)}
public abstract class {typeName} : SafeHandle, IEquatable<{typeName}>
{{
    public sealed override bool IsInvalid => handle == IntPtr.Zero;

    protected {typeName}(bool ownsHandle) : base(IntPtr.Zero, ownsHandle) {{ }}

    {RenderCopyFunction(record)}

    public {ownedHandleTypeName} OwnedCopy()
    {{
        {RenderCopyStatement(record, "ptr", "handle")}
        return new {ownedHandleTypeName}(ptr);
    }}

    public {unownedHandleTypeName} UnownedCopy()
    {{
        {RenderCopyStatement(record, "ptr", "handle")}
        return new {unownedHandleTypeName}(ptr);
    }}

    public bool Equals({typeName}? other)
    {{
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return handle.Equals(other.handle);
    }}

    public override bool Equals(object? obj)
    {{
        return ReferenceEquals(this, obj) || obj is {typeName} other && Equals(other);
    }}

    public override int GetHashCode()
    {{
        return handle.GetHashCode();
    }}
}}

public class {unownedHandleTypeName} : {typeName}
{{
    private static {unownedHandleTypeName}? nullHandle;
    public static {unownedHandleTypeName} NullHandle => nullHandle ??= new {unownedHandleTypeName}();

    /// <summary>
    /// Creates a new instance of {unownedHandleTypeName}. Used automatically by PInvoke.
    /// </summary>
    internal {unownedHandleTypeName}() : base(false) {{ }}

    /// <summary>
    /// Creates a new instance of {ownedHandleTypeName}. Assumes that the given pointer is unowned by the runtime.
    /// </summary>
    public {unownedHandleTypeName}(IntPtr ptr) : base(false)
    {{
        SetHandle(ptr);
    }}

    protected override bool ReleaseHandle()
    {{
        throw new System.Exception(""UnownedHandle must not be freed"");
    }}
}}

public partial class {ownedHandleTypeName} : {typeName}
{{
    /// <summary>
    /// Creates a new instance of {ownedHandleTypeName}. Used automatically by PInvoke.
    /// </summary>
    internal {ownedHandleTypeName}() : base(true) {{ }}

    /// <summary>
    /// Creates a new instance of {ownedHandleTypeName}. Assumes that the given pointer is owned by the runtime.
    /// </summary>
    public {ownedHandleTypeName}(IntPtr ptr) : base(true)
    {{
        SetHandle(ptr);
    }}

   {RenderFreeFunction(record)}

    /// <summary>
    /// Create a {ownedHandleTypeName} from a pointer that is assumed unowned. To do so a
    /// boxed copy is created of the given pointer to be used as the handle.
    /// </summary>
    /// <param name=""ptr"">A pointer to a {record.Name} which is not owned by the runtime.</param>
    /// <returns>A {ownedHandleTypeName}</returns>
    public static {ownedHandleTypeName} FromUnowned(IntPtr ptr)
    {{
        {RenderCopyStatement(record, "ownedPtr", "ptr")}
        return new {ownedHandleTypeName}(ownedPtr);
    }}

    internal void SetMemoryPressure()
    {{
        AddMemoryPressure();
    }}

    partial void AddMemoryPressure();
    partial void RemoveMemoryPressure();

    protected override bool ReleaseHandle()
    {{
        RemoveMemoryPressure();
        {RenderFreeStatement(record, "handle")}
        return true;
    }}
}}";
    }

    private static string RenderCopyFunction(GirModel.Record record)
    {
        return record.CopyFunction is null || !Method.IsValidCopyFunction(record.CopyFunction)
            ? string.Empty
            : $"""
                  [DllImport(ImportResolver.Library, EntryPoint = "{record.CopyFunction}")]
                  protected static extern IntPtr Copy(IntPtr handle);
               """;
    }

    private static string RenderFreeFunction(GirModel.Record record)
    {
        return record.FreeFunction is null || !Method.IsValidFreeFunction(record.FreeFunction)
            ? string.Empty
            : $"""
                   [DllImport(ImportResolver.Library, EntryPoint = "{record.FreeFunction}")]
                   private static extern void Free(IntPtr handle);
               """;
    }

    private static string RenderCopyStatement(GirModel.Record record, string resultVariable, string parameterVariable)
    {
        var getGType = $"{Model.OpaqueTypedRecord.GetFullyQualifiedInternalClassName(record)}.{Function.GetGType}()";

        return record.CopyFunction is null || !Method.IsValidCopyFunction(record.CopyFunction)
            ? $"var {resultVariable} = GObject.Internal.Functions.BoxedCopy({getGType}, {parameterVariable});"
            : $"var {resultVariable} = Copy({parameterVariable});";
    }

    private static string RenderFreeStatement(GirModel.Record record, string parameterVariable)
    {
        var getGType = $"{Model.OpaqueTypedRecord.GetFullyQualifiedInternalClassName(record)}.{Function.GetGType}()";

        return record.FreeFunction is null || !Method.IsValidFreeFunction(record.FreeFunction)
            ? $"GObject.Internal.Functions.BoxedFree({getGType}, {parameterVariable});"
            : $"Free({parameterVariable});";
    }
}
