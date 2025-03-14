using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class ClassStruct
{
    public static string Render(GirModel.Class cls)
    {
        return $@"
using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

#nullable enable

namespace {Namespace.GetInternalName(cls.Namespace)};

// AUTOGENERATED FILE - DO NOT MODIFY

{PlatformSupportAttribute.Render(cls as GirModel.PlatformDependent)}
[StructLayout(LayoutKind.Sequential)]
public partial struct {Class.GetInternalStructName(cls)}
{{
    {Fields.Render(cls.Fields)}
}}";
    }
}
