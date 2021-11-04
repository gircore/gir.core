namespace Generator3.Generation.Framework
{
    public class NativeDllImportModel
    {
        public string NamespaceName { get; }
        public string LibraryName { get; }

        public string WindowsDll { get; }
        public string LinuxDll { get; }
        public string OsxDll { get; }

        public NativeDllImportModel(string sharedLibrary, string @namespace)
        {
            this.LibraryName = @namespace;
            this.NamespaceName = @namespace + ".Native";

            var dllImportResolver = new DllImportResolver(sharedLibrary, @namespace);
            
            WindowsDll = dllImportResolver.GetWindowsDllImport();
            LinuxDll = dllImportResolver.GetLinuxDllImport();
            OsxDll = dllImportResolver.GetOsxDllImport();
        }
    }
}
