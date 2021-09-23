using System.Threading.Tasks;
using Scriban;
using Scriban.Parsing;
using Scriban.Runtime;

namespace Generator3.Rendering.Scriban
{
    public class ScribanTemplateLoader : ITemplateLoader
    {
        public string GetPath(TemplateContext context, SourceSpan callerSpan, string templateName) 
            => templateName;

        public string Load(TemplateContext context, SourceSpan callerSpan, string templatePath)
            => TemplateRessourceLoader.Read(templatePath);

        public ValueTask<string> LoadAsync(TemplateContext context, SourceSpan callerSpan, string templatePath)
            => new (TemplateRessourceLoader.Read(templatePath));
    }
}
