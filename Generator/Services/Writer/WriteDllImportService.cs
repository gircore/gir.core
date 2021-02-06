using Generator.Factories;
using Generator.Writer;
using Repository;
using Scriban.Runtime;

#nullable enable

namespace Generator.Services.Writer
{
    public interface IWriteDllImportService
    {
        void WriteDllImport(ILoadedProject loadedProject);
    }

    public class WriteDllImportService : IWriteDllImportService
    {
        private readonly IWriteHelperService _writeHelperService;
        private readonly IDllImportResolverFactory _dllImportResolverFactory;

        public WriteDllImportService(IWriteHelperService writeHelperService, IDllImportResolverFactory dllImportResolverFactory)
        {
            _writeHelperService = writeHelperService;
            _dllImportResolverFactory = dllImportResolverFactory;
        }

        public void WriteDllImport(ILoadedProject loadedProject)
        {
            IDllImportResolver dllImportResolver = _dllImportResolverFactory.Create(
                sharedLibrary: loadedProject.Namespace.SharedLibrary,
                namespaceName: loadedProject.Namespace.Name
            );

            var scriptObject = new ScriptObject
            {
                { "namespace", loadedProject.Namespace}, 
                { "windows_dll", dllImportResolver.GetWindowsDllImport() }, 
                { "linux_dll", dllImportResolver.GetLinuxDllImport() }, 
                { "osx_dll", dllImportResolver.GetOsxDllImport() }
            };

            _writeHelperService.Write(
                projectName: loadedProject.Name,
                templateName: "dll_import.sbntxt",
                folder: "Classes",
                fileName: "DllImport",
                scriptObject: scriptObject
            );
        }
    }
}
