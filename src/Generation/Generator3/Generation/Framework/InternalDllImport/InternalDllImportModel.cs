using Generator3.Converter;

namespace Generator3.Generation.Framework
{
    public class InternalDllImportModel
    {
        private readonly GirModel.Namespace _ns;

        public string NamespaceName => _ns.GetInternalName();
        public string LibraryName => _ns.Name;

        public string WindowsDll { get; }
        public string LinuxDll { get; }
        public string OsxDll { get; }

        public InternalDllImportModel(GirModel.Namespace ns)
        {
            _ns = ns;

            var dllImportResolver = new DllImportResolver(ns.SharedLibrary, ns.Name);

            WindowsDll = dllImportResolver.GetWindowsDllImport();
            LinuxDll = dllImportResolver.GetLinuxDllImport();
            OsxDll = dllImportResolver.GetOsxDllImport();
        }
    }
}
