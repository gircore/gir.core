namespace Generator3.Generation.Framework
{
    public class InternalDllImportModel
    {
        public string NamespaceName { get; }
        public string LibraryName { get; }

        public string WindowsDll { get; }
        public string LinuxDll { get; }
        public string OsxDll { get; }

        public InternalDllImportModel(string sharedLibrary, string @namespace)
        {
            this.LibraryName = @namespace;
            this.NamespaceName = @namespace + ".Internal";

            var dllImportResolver = new DllImportResolver(sharedLibrary, @namespace);
            
            WindowsDll = dllImportResolver.GetWindowsDllImport();
            LinuxDll = dllImportResolver.GetLinuxDllImport();
            OsxDll = dllImportResolver.GetOsxDllImport();
        }
    }
}
