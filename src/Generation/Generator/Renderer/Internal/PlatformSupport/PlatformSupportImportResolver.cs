namespace Generator.Renderer.Internal;

internal static class PlatformSupportImportResolver
{
    public static string Render(GirModel.Namespace? linuxNamespace, GirModel.Namespace? macosNamespace, GirModel.Namespace? windowsNamespace)
    {
        var model = new ImportResolverModel(linuxNamespace, macosNamespace, windowsNamespace);

        return $@"
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable

namespace {model.InternalNamespaceName};

internal static class ImportResolver
{{    
    public const string Library = ""{model.NamespaceName}"";
    private const {model.WindowsType} WindowsLibraryName = {model.WindowsDll};
    private const {model.LinuxType} LinuxLibraryName = {model.LinuxDll};
    private const {model.OsxType} OsxLibraryName = {model.OsxDll};

    private static IntPtr TargetLibraryPointer = IntPtr.Zero;

    public static void RegisterAsDllImportResolver()
    {{
        NativeLibrary.SetDllImportResolver(typeof(ImportResolver).Assembly, Resolve);
    }}    

    public static IntPtr Resolve(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {{
        if(libraryName != Library)
            return IntPtr.Zero;

        if (TargetLibraryPointer != IntPtr.Zero)
            return TargetLibraryPointer;

        var osDependentLibraryName = GetOsDependentLibraryName();
        TargetLibraryPointer = NativeLibrary.Load(osDependentLibraryName, assembly, searchPath);

        return TargetLibraryPointer;
    }}
    
    private static string GetOsDependentLibraryName()
    {{
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {AssignOsDependentLibraryNameWindows(model)}

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {AssignOsDependentLibraryNameOsx(model)}

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {AssignOsDependentLibraryNameLinux(model)}

        throw new System.Exception(""Unknown platform"");
    }}
}}";
    }

    private static string AssignOsDependentLibraryNameLinux(ImportResolverModel model)
    {
        return model.SupportsLinux
            ? "return LinuxLibraryName;"
            : $"throw new System.Exception(\"Linux is not supported for library {model.NamespaceName}\");";
    }

    private static string AssignOsDependentLibraryNameOsx(ImportResolverModel model)
    {
        return model.SupportsOSX
            ? "return OsxLibraryName;"
            : $"throw new System.Exception(\"OSX is not supported for library {model.NamespaceName}\");";
    }

    private static string AssignOsDependentLibraryNameWindows(ImportResolverModel model)
    {
        return model.SupportsWindows
            ? "return WindowsLibraryName;"
            : $"throw new System.Exception(\"Windows is not supported for library {model.NamespaceName}\");";
    }
}
