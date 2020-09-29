using System;
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
            foreach (var dlg in delegates)
            {
                Generate(dlg,
                    templateName: "delegate",
                    subfolder: "Delegates",
                    fileName: dlg.Name,
                    scriptObject: ScriptObject
                );
            }
        }

        protected override void GenerateStructs(IEnumerable<GRecord> records, string @namespace)
        {
            foreach (var record in records)
            {
                // By calling GetStructType(), we determine whether the struct is
                // readable or opaque and generate it accordingly. See GetStructType()
                // for details.
                
                var (templateName, subfolder) = GetStructType(record) switch
                {
                    StructType.RefStruct => ("struct", "Structs"),
                    StructType.OpaqueStruct => ("struct_as_class", "Classes"),
                    _ => throw new NotImplementedException($"Cannot generate struct {record.Name} - Skipping"),
                };

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