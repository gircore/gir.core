using System;
using System.IO;
using Scriban;
using Scriban.Parsing;

namespace Generator
{
    public class WrapperTemplateLoader : TemplateLoader
    {
        #region Methods

        public override string GetPath(TemplateContext context, SourceSpan callerSpan, string templateName)
            => Path.Combine(Environment.CurrentDirectory + "/../Generator/Templates/Wrapper/", templateName);

        #endregion
    }
}
