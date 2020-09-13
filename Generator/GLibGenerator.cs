using System.Collections.Generic;
using System.Linq;
using Gir;

namespace Generator
{
    public class GLibGenerator : Generator<GLibTemplateLoader>
    {
        public GLibGenerator(Project project) : base(project) { }

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
                // There are structs which must be generated as classes. Currently
                // this is especially true for GLib. But there a structs which actually
                // are structs. The only distinction right now is that the "fake" structs
                // have no fields defined

                var hasFields = record.Fields.Any();
                var templateName = hasFields ? "struct" : "struct_as_class";
                var subfolder = hasFields ? "Structs" : "Classes";

                Generate(record,
                    templateName: templateName,
                    subfolder: subfolder,
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
    }
}