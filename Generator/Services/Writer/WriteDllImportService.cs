using System;
using Generator.Factories;
using Repository;
using Repository.Model;
using Scriban.Runtime;

namespace Generator.Services.Writer
{
    internal class WriteDllImportService
    {
        private readonly WriteHelperService _writeHelperService;
        private readonly DllImportResolverFactory _dllImportResolverFactory;

        public WriteDllImportService(WriteHelperService writeHelperService, DllImportResolverFactory dllImportResolverFactory)
        {
            _writeHelperService = writeHelperService;
            _dllImportResolverFactory = dllImportResolverFactory;
        }

        public void WriteDllImport(Namespace ns, string outputDir)
        {
            if(ns.SharedLibrary is null)
                throw new Exception($"Namespace {ns.Name} does not provide a shared libraryinfo");
            
            DllImportResolver dllImportResolver = _dllImportResolverFactory.Create(
                sharedLibrary: ns.SharedLibrary,
                namespaceName: ns.Name
            );

            var scriptObject = new ScriptObject
            {
                { "namespace", ns}, 
                { "windows_dll", dllImportResolver.GetWindowsDllImport() }, 
                { "linux_dll", dllImportResolver.GetLinuxDllImport() }, 
                { "osx_dll", dllImportResolver.GetOsxDllImport() }
            };

            try
            {
                _writeHelperService.Write(
                    projectName: ns.ToCanonicalName(),
                    templateName: "dll_import.sbntxt",
                    folder: Folder.Classes,
                    outputDir: outputDir,
                    fileName: "DllImport",
                    scriptObject: scriptObject
                );
                
                _writeHelperService.Write(
                    projectName: ns.ToCanonicalName(),
                    templateName: "module_dll_import.sbntxt",
                    folder: Folder.Classes,
                    outputDir: outputDir,
                    fileName: "Module",
                    scriptObject: scriptObject
                );
            }
            catch (Exception ex)
            {
                Log.Error($"Could not write dll import for {ns.Name}: {ex.Message}");
            }
        }
    }
}
