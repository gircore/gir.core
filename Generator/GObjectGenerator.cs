using System.Collections.Generic;
using System.Linq;
using Gir;
using Scriban.Runtime;

namespace Generator
{
    public class GObjectGenerator : Generator<GObjectTemplateLoader>
    {
        public GObjectGenerator(Project project) : base(project) { }

        protected override void GenerateDelegates(IEnumerable<GCallback> delegates, string @namespace)
        {
            foreach (var dele in delegates)
            {
                var scriptObject = GetScriptObject();
                scriptObject.Import(dele);

                Generate(
                    templateName: "delegate",
                    subfolder: "Delegates",
                    fileName: dele.Name,
                    scriptObject: scriptObject
                );
            }
        }

        protected override void GenerateStructs(IEnumerable<GRecord> records, string @namespace)
        {
            foreach (var record in records)
            {
                var scriptObject = GetScriptObject();
                scriptObject.Import(record);

                Generate(
                    templateName: "struct",
                    subfolder: "Structs",
                    fileName: record.Name,
                    scriptObject: scriptObject
                );
            }
        }

        protected override void GenerateClasses(IEnumerable<GInterface> classes, string @namespace)
        {
            foreach (var cls in classes)
            {
                var scriptObject = GetScriptObject();
                scriptObject.Import(cls);

                Generate(
                    templateName: "class",
                    subfolder: "Classes",
                    fileName: cls.Name,
                    scriptObject: scriptObject
                );
            }
        }

        protected override void GenerateEnums(IEnumerable<GEnumeration> enums, string @namespace, bool hasFlags)
        {
            foreach (var obj in enums)
            {
                var scriptObject = GetScriptObject();
                scriptObject.Import(obj);
                scriptObject.Add("has_flags", hasFlags);

                Generate(
                    templateName: "enum",
                    subfolder: "Enums",
                    fileName: obj.Name,
                    scriptObject: scriptObject
                );
            }
        }

        protected override void GenerateGlobals(IEnumerable<GMethod> methods, string @namespace)
        {
            var list = methods.ToList();
            RemoveVarArgsMethods(list);
            var scriptObject = GetScriptObject();
            scriptObject.Add("methods", list);

            Generate(
                templateName: "global",
                subfolder: "Classes",
                fileName: "Global",
                scriptObject: scriptObject
            );
        }
    }
}