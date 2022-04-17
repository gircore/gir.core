using System;
using System.Collections.Generic;
using GirModel;

namespace Generator3.Renderer;

public static class PlatformSupportRenderer
{
    public static string RenderPlatformSupportAttributes(this PlatformDependent? platformDependent)
    {
        if (platformDependent is null)
            return string.Empty;

        var attributes = new List<string>();

        if (!platformDependent.SupportsLinux)
            attributes.Add("[UnsupportedOSPlatform(\"Linux\")]");

        if (!platformDependent.SupportsMacos)
            attributes.Add("[UnsupportedOSPlatform(\"OSX\")]");

        if (!platformDependent.SupportsWindows)
            attributes.Add("[UnsupportedOSPlatform(\"Windows\")]");

        return string.Join(Environment.NewLine, attributes);
    }

    public static string RenderPlatformSupportCondition(this PlatformDependent? platformDependent)
    {
        if (platformDependent is null)
            return string.Empty;

        if (platformDependent.SupportsLinux && platformDependent.SupportsMacos && platformDependent.SupportsWindows)
            return string.Empty; //All platforms supported

        var statements = new List<string>();

        if (platformDependent.SupportsLinux)
            statements.Add("RuntimeInformation.IsOSPlatform(OSPlatform.Linux)");

        if (platformDependent.SupportsMacos)
            statements.Add("RuntimeInformation.IsOSPlatform(OSPlatform.OSX)");

        if (platformDependent.SupportsWindows)
            statements.Add("RuntimeInformation.IsOSPlatform(OSPlatform.Windows)");

        return $"if({string.Join(" || ", statements)})";
    }
}
