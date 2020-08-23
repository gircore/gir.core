using System;
using System.Collections.Generic;
using Gir;
using Scriban;
using Scriban.Runtime;

namespace Generator
{
    public class CoreGenerator : Generator
    {
        public CoreGenerator(string girFile, string outputDir) : base(girFile, outputDir) { }

        protected override void Generate(GNamespace gNamespace)
        {
            if (gNamespace.Name is null)
                throw new Exception("Could not create code. Namespace is missing a name.");
            
            GenerateClasses(gNamespace.Classes, gNamespace.Name);
        }

        private void GenerateClasses(IEnumerable<GInterface> classes, string @namespace)
        {
            foreach (var cls in classes)
            {
                var scriptObject = new ScriptObject {{"namespace", @namespace}};
                scriptObject.Import(cls);

                Create("class", cls.Name + ".Generated.cs", scriptObject);
            }
        }

        private void Create(string templateFile, string fileName, ScriptObject scriptObject)
        {
            var context = new TemplateContext {TemplateLoader = new CoreTemplateLoader()};
            context.PushGlobal(scriptObject);
            
            templateFile = $"../Generator/Templates/Core/{templateFile}.sbntxt";
            GenerateCode(templateFile, fileName, context);
        }
    }
}
