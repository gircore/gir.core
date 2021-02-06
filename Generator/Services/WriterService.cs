using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Generator.Factories;
using Generator.Services;
using Repository;
using Repository.Analysis;
using Repository.Model;
using Scriban;
using Scriban.Runtime;

#nullable enable

namespace Generator
{
    public interface IWriterService
    {
        Task WriteAsync(ILoadedProject loadedProject);
    }

    public class WriterService : IWriterService
    {
        private readonly ITemplateReaderService _templateReaderService;
        private readonly IDllImportResolverFactory _dllImportResolverFactory;

        public WriterService(ITemplateReaderService templateReaderService, IDllImportResolverFactory dllImportResolverFactory)
        {
            _templateReaderService = templateReaderService;
            _dllImportResolverFactory = dllImportResolverFactory;
        }

        public Task WriteAsync(ILoadedProject loadedProject)
        {
            return Task.Run(() => Write(loadedProject));
        }

        public void Write(ILoadedProject loadedProject)
        {
            WriteDllImport(loadedProject);
            
            WriteTypes(
                projectName: loadedProject.Name,
                templateName: "delegate.sbntxt",
                subfolder: "Delegates",
                objects: loadedProject.Namespace.Callbacks
            );

            WriteTypes(
                projectName: loadedProject.Name,
                templateName: "class.sbntxt",
                subfolder: "Classes",
                objects: loadedProject.Namespace.Classes
            );
        }

        private void WriteDllImport(ILoadedProject loadedProject)
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
            
            Write(
                projectName: loadedProject.Name,
                templateName: "dll_import.sbntxt",
                folder: "Classes",
                fileName: "DllImport",
                scriptObject: scriptObject
            );
        }

        private void WriteTypes(string projectName, string templateName, string subfolder, IEnumerable<IType> objects)
        {
            foreach (var obj in objects)
            {
                var scriptObject = new ScriptObject();
                scriptObject.Import(obj);
                scriptObject.Import("write_native_arguments", new Func<IEnumerable<Argument>, string>(TemplateWriter.WriteNativeArguments));
                scriptObject.Import("write_native_symbol_reference", new Func<ISymbolReference, string>(TemplateWriter.WriteNativeSymbolReference));
                scriptObject.Import("write_native_method", new Func<Method, string>(TemplateWriter.WriteNativeMethod));

                scriptObject.Import("write_inheritance", new Func<ISymbolReference?, IEnumerable<ISymbolReference>, string>(TemplateWriter.WriteInheritance));
                
                Write(
                    projectName: projectName,
                    templateName: templateName,
                    folder: subfolder,
                    fileName: obj.ManagedName,
                    scriptObject: scriptObject
                );
            }
        }
        
        private void Write(string projectName, string templateName, string folder, string fileName, ScriptObject scriptObject)
        {
            var template = _templateReaderService. ReadGenericTemplate(templateName);

            WriteCode(
                folder: CreateSubfolder(projectName, folder),
                filename: fileName,
                code: GenerateCode(template, scriptObject)
            );
        }
        
        private static void WriteCode(string folder, string filename, string code)
        {
            var path = Path.Combine(folder, $"{filename}.Generated.cs");
            File.WriteAllText(path, code);
        }

        private static string CreateSubfolder(string projectName, string subfolder)
        {
            var dir = $"output/{projectName}/{subfolder}/";
            Directory.CreateDirectory(dir);

            return dir;
        }

        private static string GenerateCode(Template template, ScriptObject scriptObject)
        {
            var templateContext = new TemplateContext {IndentWithInclude = true, TemplateLoader = new TemplateLoader()};

            templateContext.PushGlobal(scriptObject);
            return template.Render(templateContext);
        }
    }
}
