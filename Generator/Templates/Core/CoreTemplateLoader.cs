using System;
using System.IO;
using Scriban;
using Scriban.Parsing;

namespace Generator
{
    public class CoreTemplateLoader : TemplateLoader
    {
        public override string GetPath(TemplateContext context, SourceSpan callerSpan, string templateName)
            => Path.Combine(Environment.CurrentDirectory + "/../Generator/Templates/Core/", templateName);
    }
}
