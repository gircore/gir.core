using System.IO;
using Generator.Services;
using Scriban;
using Scriban.Runtime;

#nullable enable

namespace Generator.Services.Writer
{
    public class WriteHelperService
    {
        private readonly TemplateReaderService _templateReaderService;

        public WriteHelperService(TemplateReaderService templateReaderService)
        {
            _templateReaderService = templateReaderService;
        }

        public void Write(string projectName, string templateName, string folder, string outputDir, string fileName, ScriptObject scriptObject)
        {
            var template = _templateReaderService. ReadGenericTemplate(templateName);

            WriteCode(
                folder: CreateSubfolder(projectName, outputDir, folder),
                filename: fileName,
                code: GenerateCode(template, scriptObject)
            );
        }
        
        private static void WriteCode(string folder, string filename, string code)
        {
            var path = Path.Combine(folder, $"{filename}.Generated.cs");
            File.WriteAllText(path, code);
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
