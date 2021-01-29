using System;
using System.IO;
using Scriban;
using Scriban.Parsing;

namespace Generator
{
    public class GObjectTemplateLoader : TemplateLoader
    {
        #region Methods

        public override string GetPath(TemplateContext context, SourceSpan callerSpan, string templateName)
        {
            var file = Path.Combine(Environment.CurrentDirectory + "/../Generator/Templates/GObject/", templateName);

            if (!File.Exists(file))
                return base.GetPath(context, callerSpan, templateName);

            return file;
        }

        #endregion
    }
}
