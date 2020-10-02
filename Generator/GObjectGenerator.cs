using System.Collections.Generic;
using System.Linq;
using Gir;

namespace Generator
{
    public class GObjectGenerator : Generator<GObjectTemplateLoader>
    {
        public GObjectGenerator(Project project) : base(project) { }

        protected override void GenerateDelegates(IEnumerable<GCallback> delegates, string @namespace)
        {
            foreach (var dele in delegates)
            {
                Generate(dele,
                    templateName: "delegate",
                    subfolder: "Delegates",
                    fileName: dele.Name,
                    scriptObject: ScriptObject
                );
            }
        }

        protected override void GenerateStructs(IEnumerable<GRecord> records, string @namespace)
        {
            foreach (var record in records)
            {
                Generate(record,
                    templateName: "struct",
                    subfolder: "Structs",
                    fileName: record.Name,
                    scriptObject: ScriptObject
                );
            }
        }

        protected override void GenerateClasses(IEnumerable<GInterface> classes, string @namespace)
        {
            foreach (var cls in classes)
            {
                Generate(cls,
                    templateName: "class",
                    subfolder: "Classes",
                    fileName: cls.Name,
                    scriptObject: ScriptObject
                );
            }
        }

        protected override void GenerateEnums(IEnumerable<GEnumeration> enums, string @namespace, bool hasFlags)
        {
            ScriptObject.Add("has_flags", hasFlags);
            
            foreach (var obj in enums)
            {
                Generate(obj,
                    templateName: "enum",
                    subfolder: "Enums",
                    fileName: obj.Name,
                    scriptObject: ScriptObject
                );
            }
        }

        protected override void GenerateGlobals(IEnumerable<GMethod> methods, string @namespace)
        {
            var list = methods.ToList();
            RemoveVarArgsMethods(list);
            ScriptObject.Add("methods", list);
            
            Generate(
                templateName: "global",
                subfolder: "Classes",
                fileName: "Global",
                scriptObject: ScriptObject
            );
        }
    }
}