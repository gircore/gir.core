using Scriban;
using Scriban.Runtime;

namespace Gir.Integration.CSharp;

public class TemplateProvider
{
    public string Get(string templateName, object data)
    {
        var templateContent = GetTemplateContent(templateName);
        var template = Template.Parse(templateContent);

        TemplateContext context = GetTemplateContext(data);
        var content = template.Render(context);

        return content;
    }

    private TemplateContext GetTemplateContext(object data)
    {
        var scriptObject = new ScriptObject();
        scriptObject.Import(data);

        var context = new TemplateContext();
        context.PushGlobal(scriptObject);
        context.IndentWithInclude = true;

        return context;
    }

    private string GetTemplateContent(string templateName)
    {
        return typeof(TemplateProvider).Assembly.ReadResourceAsString(templateName);
    }
}
