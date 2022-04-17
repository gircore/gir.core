namespace Generator3.Generation.Framework
{
    public class InternalDllImportTemplate : Template<InternalDllImportModel>
    {
        public string Render(InternalDllImportModel model)
        {
            return $@"
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable

namespace {model.InternalNamespaceName}
{{
    internal static class DllImport
    {{
        #region Fields
        
        private const {model.WindowsType} _windowsDllName = {model.WindowsDll};
        private const {model.LinuxType} _linuxDllName = {model.LinuxDll};
        private const {model.OsxType} _osxDllName = {model.OsxDll};

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
            if (libraryName != ""{model.NamespaceName}"")
            {{
                osDependentLibraryName = null;
                return false;
            }}

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {{
                {AssignOsDependentLibraryNameWindows(model)}
            }}
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {{
                {AssignOsDependentLibraryNameOSX(model)}
            }}
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {{
                {AssignOsDependentLibraryNameLinux(model)}
            }}
            else
                throw new Exception(""Unknown platform"");
        
            return true;
        }}
        
        #endregion
    }}
}}";
        }

        private string AssignOsDependentLibraryNameLinux(InternalDllImportModel model)
        {
            return model.SupportsLinux
                ? "osDependentLibraryName = _linuxDllName;"
                : $"throw new Exception(\"Linux is not supported for library {model.NamespaceName}\");";
        }

        private string AssignOsDependentLibraryNameOSX(InternalDllImportModel model)
        {
            return model.SupportsOSX
                ? "osDependentLibraryName = _osxDllName;"
                : $"throw new Exception(\"OSX is not supported for library {model.NamespaceName}\");";
        }

        private string AssignOsDependentLibraryNameWindows(InternalDllImportModel model)
        {
            return model.SupportsWindows
                ? "osDependentLibraryName = _windowsDllName;"
                : $"throw new Exception(\"Windows is not supported for library {model.NamespaceName}\");";
        }
    }
}
