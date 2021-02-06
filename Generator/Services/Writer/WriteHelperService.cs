using System.IO;
using Generator.Services;
using Scriban;
using Scriban.Runtime;

#nullable enable

namespace Generator.Writer
{
    public interface IWriteHelperService
    {
        void Write(string projectName, string templateName, string folder, string fileName, ScriptObject scriptObject);
    }

    public class WriteHelperService : IWriteHelperService
    {
        private readonly ITemplateReaderService _templateReaderService;

        public WriteHelperService(ITemplateReaderService templateReaderService)
        {
            _templateReaderService = templateReaderService;
        }

        public void Write(string projectName, string templateName, string folder, string fileName, ScriptObject scriptObject)
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
