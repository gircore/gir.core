using System;
using Generator.Factories;
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
            if(loadedProject.Namespace.SharedLibrary is null)
                throw new Exception($"Namespace {loadedProject.Namespace.Name} does not provide a shared libraryinfo");
            
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

            try
            {
                _writeHelperService.Write(
                    projectName: loadedProject.Name,
                    templateName: "dll_import.sbntxt",
                    folder: "Classes",
                    fileName: "DllImport",
                    scriptObject: scriptObject
                );
            }
            catch (Exception ex)
            {
                Log.Error($"Could not write dll import for {loadedProject.Name}: {ex.Message}");
            }
        }
    }
}
