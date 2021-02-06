using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
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

        public WriterService(ITemplateReaderService templateReaderService)
        {
            _templateReaderService = templateReaderService;
        }

        public Task WriteAsync(ILoadedProject loadedProject)
        {
            return Task.Run(() => Write(loadedProject));
        }

        public void Write(ILoadedProject loadedProject)
        {
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
        
        private void WriteTypes(string projectName, string templateName, string subfolder, IEnumerable<IType> objects)
        {
            var template = _templateReaderService. ReadGenericTemplate(templateName);
            var dir = CreateSubfolder(projectName, subfolder);
            GenerateTypes(template, dir, objects);
        }

        private static string CreateSubfolder(string projectName, string subfolder)
        {
            var dir = $"output/{projectName}/{subfolder}/";
            Directory.CreateDirectory(dir);

            return dir;
        }

        private static void GenerateTypes(Template template, string folder, IEnumerable<IType> objects)
        {
            // Generate a file for each class
            foreach (IType obj in objects)
            {
                var scriptObject = new ScriptObject();
                scriptObject.Import(obj);
                scriptObject.Import("write_native_arguments", new Func<IEnumerable<Argument>, string>(TemplateWriter.WriteNativeArguments));
                scriptObject.Import("write_native_symbol_reference", new Func<ISymbolReference, string>(TemplateWriter.WriteNativeSymbolReference));
                scriptObject.Import("write_native_method", new Func<Method, string>(TemplateWriter.WriteNativeMethod));
                
                scriptObject.Import("write_inheritance", new Func<ISymbolReference?, IEnumerable<ISymbolReference>, string>(TemplateWriter.WriteInheritance));
                
                var templateContext = new TemplateContext
                {
                    IndentWithInclude = true,
                    TemplateLoader = new TemplateLoader()
                };

                templateContext.PushGlobal(scriptObject);
                var result = template.Render(templateContext);

                var path = Path.Combine(folder, $"{obj.ManagedName}.Generated.cs");
                File.WriteAllText(path, result);
            }
        }

    }
}
