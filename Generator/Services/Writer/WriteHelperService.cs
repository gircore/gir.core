using System.IO;
using Scriban;
using Scriban.Runtime;

namespace Generator.Services.Writer
{
    internal class WriteHelperService
    {
        private readonly TemplateReaderService _templateReaderService;

        public WriteHelperService(TemplateReaderService templateReaderService)
        {
            _templateReaderService = templateReaderService;
        }

        public void Write(string projectName, string templateName, string folder, string outputDir, string fileName, ScriptObject scriptObject)
        {
            var template = _templateReaderService. ReadGenericTemplate(templateName);

            WriteCodeAsync(
                folder: CreateSubfolder(projectName, outputDir, folder),
                filename: fileName,
                code: GenerateCode(template, scriptObject)
            );
        }
        
        private static async void WriteCodeAsync(string folder, string filename, string code)
        {
            var path = Path.Combine(folder, $"{filename}.Generated.cs");
            await File.WriteAllTextAsync(path, code);
        }

        private static string CreateSubfolder(string projectName, string outputDir, string subfolder)
        {
            var dir = $"{outputDir}/{projectName}/{subfolder}/";
            Directory.CreateDirectory(dir);

            return dir;
        }

        private static string GenerateCode(Template template, ScriptObject scriptObject)
        {
            var templateContext = new TemplateContext
            {
                IndentWithInclude = true, 
                TemplateLoader = new TemplateLoader(),
                LoopLimit = 10000 // Some libraries define more than 1000 elements (e.g. GDK constants)
            };

            templateContext.PushGlobal(scriptObject);
            return template.Render(templateContext);
        }
    }
}
