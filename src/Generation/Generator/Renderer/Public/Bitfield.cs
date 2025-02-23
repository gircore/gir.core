using System;
using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Public;

internal static class Bitfield
{
    public static string Render(GirModel.Bitfield bitfield)
    {
        return $@"
using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

#nullable enable

namespace {Namespace.GetPublicName(bitfield.Namespace)};

// AUTOGENERATED FILE - DO NOT MODIFY

{PlatformSupportAttribute.Render(bitfield as GirModel.PlatformDependent)}
[Flags]
public enum {bitfield.Name} : uint
{{
    {bitfield
        .Members
        .Where(Member.IsEnabled)
        .Select(MemberRenderer.Render)
        .Join(Environment.NewLine)}
}}";
    }
}
