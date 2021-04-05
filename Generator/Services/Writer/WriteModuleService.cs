using System;
using System.Diagnostics;
using System.Linq;
using Generator.Factories;
using Repository.Model;
using Scriban.Runtime;

namespace Generator.Services.Writer
{
    internal class WriteModuleService
    {
        private readonly WriteHelperService _writeHelperService;
        private readonly DllImportResolverFactory _dllImportResolverFactory;

        public WriteModuleService(WriteHelperService writeHelperService, DllImportResolverFactory dllImportResolverFactory)
        {
            _writeHelperService = writeHelperService;
            _dllImportResolverFactory = dllImportResolverFactory;
        }

        public void Write(Namespace ns, string outputDir)
        {
            if (ns.SharedLibrary is null)
                throw new Exception($"Namespace {ns.Name} does not provide a shared libraryinfo");

            try
            {
                WriteTypeDictionaryInitialization(ns, outputDir);
                WriteDllImport(ns, outputDir);
            }
            catch (Exception ex)
            {
                Log.Error($"Could not write module for {ns.Name}: {ex.Message}");
            }
        }

        private void WriteTypeDictionaryInitialization(Namespace ns, string outputDir)
        {
            var classes = ns.Classes.Where(x => !x.IsFundamental);

            if (!classes.Any())
                return;
            
            var scriptObject = new ScriptObject()
            {
                { "namespace", ns },
                { "classes",  classes},
            };
            scriptObject.Import("write_type_registration", new Func<Symbol, string>(s => s.WriteTypeRegistration()));

            _writeHelperService.Write(
                projectName: ns.ToCanonicalName(),
                templateName: "module_type_registration.sbntxt",
                folder: Folder.Managed.Classes,
                outputDir: outputDir,
                fileName: "Module.TypeRegistration",
                scriptObject: scriptObject
            );
        }
        
        private void WriteDllImport(Namespace ns, string outputDir)
        {
            Debug.Assert(
                condition: ns.SharedLibrary is not null,
                message: "sharedLibrary is not set"
            );

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

            _writeHelperService.Write(
                projectName: ns.ToCanonicalName(),
                templateName: "native.dll_import.sbntxt",
                folder: Folder.Managed.Classes,
                outputDir: outputDir,
                fileName: "DllImport",
                scriptObject: scriptObject
            );

            _writeHelperService.Write(
                projectName: ns.ToCanonicalName(),
                templateName: "module_dll_import.sbntxt",
                folder: Folder.Managed.Classes,
                outputDir: outputDir,
                fileName: "Module.DllImport",
                scriptObject: scriptObject
            );
        }
    }
}
