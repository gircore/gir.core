namespace Generator3.Generation.Framework
{
    public class NativeDllImportTemplate : Template<NativeDllImportModel>
    {
        public string Render(NativeDllImportModel model)
        {
            return $@"
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable

namespace {model.NamespaceName}
{{
    internal static class DllImport
    {{
        #region Fields
        
        private const string _windowsDllName = ""{model.WindowsDll}"";
        private const string _linuxDllName = ""{model.LinuxDll}"";
        private const string _osxDllName = ""{model.OsxDll}"";

        private static readonly Dictionary<string, IntPtr> _cache = new ();

        #endregion
            
        #region Methods
        
        public static void Initialize()
        {{
            NativeLibrary.SetDllImportResolver(typeof(DllImport).Assembly, ImportResolver);
        }}
        
        private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {{
            if (_cache.TryGetValue(libraryName, out var cachedLibHandle))
                return cachedLibHandle;
        
            if(!TryGetOsDependentLibraryName(libraryName, out var osDependentLibraryName))
                return IntPtr.Zero;
        
            if (NativeLibrary.TryLoad(osDependentLibraryName, assembly, searchPath, out IntPtr libHandle))
            {{
                _cache[libraryName] = libHandle;
                return libHandle;
            }}
        
            //Fall back to default dll search mechanic
            return IntPtr.Zero;
        }}
        
        private static bool TryGetOsDependentLibraryName(string libraryName, [NotNullWhen(true)] out string? osDependentLibraryName)
        {{
            if (libraryName != ""{model.LibraryName}"")
            {{
                osDependentLibraryName = null;
                return false;
            }}

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                osDependentLibraryName = _windowsDllName;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                osDependentLibraryName = _osxDllName;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                osDependentLibraryName = _linuxDllName;
            else
                throw new Exception(""Unknown platform"");
        
            return true;
        }}
        
        #endregion
    }}
}}";
        }
    }
}
