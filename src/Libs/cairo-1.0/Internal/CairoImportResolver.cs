using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

internal static class CairoImportResolver
{
    public const string Library = "cairo-graphics";

    private const string WindowsLibraryName = "libcairo-2.dll";
    private const string LinuxLibraryName = "libcairo.so.2";
    private const string OsxLibraryName = "libcairo.2.dylib";

    private static IntPtr TargetLibraryPointer = IntPtr.Zero;

    public static IntPtr Resolve(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libraryName != Library)
            return IntPtr.Zero;

        if (TargetLibraryPointer != IntPtr.Zero)
            return TargetLibraryPointer;

        var osDependentLibraryName = GetOsDependentLibraryName();
        TargetLibraryPointer = NativeLibrary.Load(osDependentLibraryName, assembly, searchPath);

        return TargetLibraryPointer;
    }

    private static string GetOsDependentLibraryName()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return WindowsLibraryName;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return OsxLibraryName;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return LinuxLibraryName;

        throw new Exception("Unknown platform");
    }
}
