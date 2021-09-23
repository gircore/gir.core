using System;
using System.Collections.Generic;
using Scriban;
using Scriban.Runtime;

namespace Generator3.Rendering.Scriban
{
    internal class TemplateRenderer
    {
        public string Render(Template template, object data)
        {
            var templateContext = GetTemplateContext(data);

            try
            {
                return template.Render(templateContext);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not render template: {ex.GetBaseException().Message}");
            }
        }

        private static TemplateContext GetTemplateContext(object data)
        {
            var templateContext = new TemplateContext
            {
                IndentWithInclude = true,
                TemplateLoader = new ScribanTemplateLoader(),
                LoopLimit = 10000 // Some libraries define more than 1000 elements (e.g. GDK constants)
            };

            var scriptObject = new ScriptObject();
            scriptObject.Import(data);

            templateContext.PushGlobal(scriptObject);

            return templateContext;
        }
    }
}
