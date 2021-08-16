using System;
using GirLoader.Output;

namespace Generator
{
    internal class DllImportResolver
    {
        private readonly string _sharedLibrary;
        private readonly NamespaceName _namespaceName;

        public DllImportResolver(string sharedLibrary, NamespaceName namespaceName)
        {
            _sharedLibrary = sharedLibrary;
            _namespaceName = namespaceName;
        }

        public string GetWindowsDllImport()
        {
            var (name, version) = ExtractLibraryData();
            return GetWindowsDllImport(name, version);
        }

        public string GetOsxDllImport()
        {
            var (name, version) = ExtractLibraryData();
            return GetOsxDllImport(name, version);
        }

        public string GetLinuxDllImport()
        {
            var (name, version) = ExtractLibraryData();
            return GetLinuxDllImport(name, version);
        }

        private (string name, string version) ExtractLibraryData()
        {
            var lib = GetLibraryName();
            return ExtractData(lib);
        }

        private string GetLibraryName()
        {
            var lib = _sharedLibrary;

            if (_sharedLibrary.Contains(","))
            {
                var libs = _sharedLibrary.Split(',');
                var result = System.Array.Find(libs, x => x.Contains(_namespaceName, StringComparison.OrdinalIgnoreCase));

                lib = result ?? throw new Exception($"Cant find dll import for {_namespaceName}, no match found in: {_sharedLibrary}");
            }

            return lib;
        }

        private static (string name, string version) ExtractData(string lib)
        {
            var lastDot = lib.LastIndexOf('.');
            var version = lib[(lastDot + 1)..];
            var name = lib[..lastDot].Replace(".so", "");

            return (name, version);
        }

        private static string GetWindowsDllImport(string name, string version)
        {
            var versionExtension = "";
            if (!string.IsNullOrEmpty(version))
                versionExtension = "-" + version;

            return name + versionExtension + ".dll";
        }

        private static string GetOsxDllImport(string name, string version)
        {
            var versionExtension = "";
            if (!string.IsNullOrEmpty(version))
                versionExtension = "." + version;

            return name + versionExtension + ".dylib";
        }

        private static string GetLinuxDllImport(string name, string version)
        {
            var versionExtension = "";
            if (!string.IsNullOrEmpty(version))
                versionExtension = "." + version;

            return name + ".so" + versionExtension;
        }
    }
}
