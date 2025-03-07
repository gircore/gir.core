using System;
using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Public;

internal static class OpaqueUntypedRecord
{
    public static string Render(GirModel.Record record)
    {
        var name = Model.OpaqueUntypedRecord.GetPublicClassName(record);
        var internalHandleName = Model.OpaqueUntypedRecord.GetFullyQuallifiedOwnedHandle(record);

        return $@"
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

#nullable enable

namespace {Namespace.GetPublicName(record.Namespace)};

// AUTOGENERATED FILE - DO NOT MODIFY

{PlatformSupportAttribute.Render(record as GirModel.PlatformDependent)}
public sealed partial class {name} {RenderIDisposableDefinition(record.FreeFunction)}
{{
    public {internalHandleName} Handle {{ get; }}

    public {name}({internalHandleName} handle)
    {{
        Handle = handle;
        Initialize();
    }}

    // Implement this to perform additional steps in the constructor
    partial void Initialize();

    {record.Constructors
        .Select(ConstructorRenderer.Render)
        .Join(Environment.NewLine)}

    {record.Functions
        .Select(FunctionRenderer.Render)
        .Join(Environment.NewLine)}

    {record.Methods
        .Where(Method.IsEnabled)
        .Select(MethodRenderer.Render)
        .Join(Environment.NewLine)}

    public bool Equals({name}? other)
    {{
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Handle.Equals(other.Handle);
    }}

    public override bool Equals(object? obj)
    {{
        return ReferenceEquals(this, obj) || obj is {name} other && Equals(other);
    }}

    public override int GetHashCode()
    {{
        return Handle.GetHashCode();
    }}

    {RenderIDisposableImplementation(record.FreeFunction)}
}}";
    }

    private static string RenderIDisposableDefinition(GirModel.Method? freeFunc)
    {
        if (freeFunc is null || !Method.IsValidFreeFunction(freeFunc))
            return string.Empty;

        return ": IDisposable";
    }

    private static string RenderIDisposableImplementation(GirModel.Method? freeFunc)
    {
        if (freeFunc is null || !Method.IsValidFreeFunction(freeFunc))
            return string.Empty;

        return """
                    public void Dispose()
                    {
                        Handle.Dispose();
                    }
               """;
    }
}
