using System.Collections.Generic;
using System.Linq;
using Gir;
using Scriban.Runtime;

namespace Generator
{
    public class GObjectGenerator : Generator<GObjectTemplateLoader>
    {
        #region Constructors

        public GObjectGenerator(Project project) : base(project) { }

        #endregion

        #region Methods

        protected override void GenerateDelegates(IEnumerable<GCallback> delegates, string @namespace)
        {
            foreach (GCallback? dele in delegates)
            {
                ScriptObject? scriptObject = GetScriptObject();
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
            foreach (GRecord? record in records)
            {
                ScriptObject? scriptObject = GetScriptObject();
                scriptObject.Import(record);

                Generate(
                    templateName: "struct",
                    subfolder: "Structs",
                    fileName: record.Name,
                    scriptObject: scriptObject
                );
            }
        }

        protected override void GenerateClasses(IEnumerable<GClass> classes, string @namespace)
        {
            foreach (GClass? cls in classes)
            {
                ScriptObject? scriptObject = GetScriptObject();
                scriptObject.Import(cls);

                Generate(
                    templateName: "class",
                    subfolder: "Classes",
                    fileName: cls.Name,
                    scriptObject: scriptObject
                );
            }
        }

        protected override void GenerateInterfaces(IEnumerable<GInterface> interfaces, string @namespace)
        {
            foreach (GInterface? iface in interfaces)
            {
                ScriptObject? scriptObject = GetScriptObject();
                scriptObject.Import(iface);

                Generate(
                    templateName: "interface",
                    subfolder: "Interfaces",
                    fileName: iface.Name,
                    scriptObject: scriptObject
                );
            }
        }

        protected override void GenerateEnums(IEnumerable<GEnumeration> enums, string @namespace, bool hasFlags)
        {
            foreach (GEnumeration? obj in enums)
            {
                ScriptObject? scriptObject = GetScriptObject();
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
            ScriptObject? scriptObject = GetScriptObject();
            scriptObject.Add("methods", methods.Where(x => !x.HasVariadicParameter()));

            Generate(
                templateName: "global",
                subfolder: "Classes",
                fileName: "Global",
                scriptObject: scriptObject
            );
        }

        protected override void GenerateConstants(IEnumerable<GConstant> constants, string @namespace)
        {
            ScriptObject? scriptObject = GetScriptObject();
            scriptObject.Add("constants", constants);

            Generate(
                templateName: "constants",
                subfolder: "Classes",
                fileName: "Constants",
                scriptObject: scriptObject
            );
        }

        #endregion
    }
}
